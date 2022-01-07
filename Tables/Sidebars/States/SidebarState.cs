using Controls.Selection;
using Microsoft.AspNetCore.Components.Routing;
using System.Collections.Concurrent;

namespace Controls.Sidebars.States
{
    public class SidebarState
    {
        private readonly ConcurrentDictionary<string, SidebarItemStack> locationLookup = new();
        private readonly SelectionCollection<NavItem> navItemCollection;

        public SidebarState(NavItemState navItemState)
        {
            navItemCollection = navItemState.NavItemCollection;
        }

        public IEnumerable<ISidebarItem> Items { get; private set; } = Enumerable.Empty<ISidebarItem>();

        public string Title { get; set; } = string.Empty;

        public void SetItems(IEnumerable<ISidebarItem> sidebarItems)
        {
            Items = sidebarItems;
            locationLookup.Clear();
            AddToLocationLookup(new List<ISidebarItem>(), sidebarItems);
        }

        private void AddToLocationLookup(List<ISidebarItem> headers, IEnumerable<ISidebarItem> sidebarItems)
        {
            foreach (var item in sidebarItems)
            {
                switch (item.ItemType)
                {
                    case SidebarItemType.NavItem:
                        var navItem = (NavItem)item;
                        if (navItem.Link is not null)
                            locationLookup.TryAdd(navItem.Link, new() { Headers = headers.ToArray(), Item = navItem });
                        break;
                    case SidebarItemType.NavHeader:
                        var navHeader = (NavHeader)item;
                        var newHeaders = headers.ToList();
                        newHeaders.Add(navHeader);
                        AddToLocationLookup(newHeaders, navHeader.Children);
                        break;
                }
            }
        }

        public Task OnNavigateAsync(NavigationContext args)
        {
            HandleNavigate(args.Path);
            return Task.CompletedTask;
        }

        private void HandleNavigate(string path)
        {
            if (locationLookup.ContainsKey(path))
            {
                var stack = locationLookup[path];
                foreach (var header in stack.Headers)
                    header.Select(true);
                navItemCollection.Select(stack.Item, true);
            }
        }

        private class SidebarItemStack
        {
            public IEnumerable<ISidebarItem> Headers { get; init; } = Enumerable.Empty<ISidebarItem>();

            public NavItem Item { get; init; }
        }
    }
}
