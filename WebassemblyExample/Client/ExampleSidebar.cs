using Controls.Sidebars;

namespace WebassemblyExample.Client;

public static class ExampleSidebar
{
    public const string Title = "Example Controls";
    public static readonly ISidebarItem[] Items = new ISidebarItem[]
    {
        new NavItem("Cookies", "cookies", string.Empty),
        new NavHeader("Data", new ISidebarItem[]
        {
            new NavItem("Tables", "tables", string.Empty),
            new NavItem("Filters", "filters", string.Empty),
            new NavItem("Pagination", "pagination", string.Empty),
            new NavItem("Selection", "selection", string.Empty),
            new NavItem("Sorting", "sorting", string.Empty),
        }),
        new NavHeader("Inputs", new ISidebarItem[]
        {
            new NavItem("Buttons", "buttons", string.Empty),
            new NavItem("Captchas", "captchas", string.Empty),
            new NavItem("Check Boxes", "checkboxes", string.Empty),
            new NavItem("Clipboard", "clipboard", string.Empty),
            new NavItem("Color Pickers", "colorpickers", string.Empty),
            new NavItem("Date Pickers", "datepickers", string.Empty),
            new NavItem("Draggables", "draggables", string.Empty),
            new NavItem("Dropdowns", "dropdowns", string.Empty),
            new NavItem("File Drops", "filedrops", string.Empty),
            new NavItem("Text", "text", string.Empty),
            new NavItem("Sliders", "sliders", string.Empty),
            //new NavItem("Text Editors", "editors", string.Empty),
        }),
        new NavHeader("Layout", new ISidebarItem[]
        {
            new NavItem("Containers", "containers", string.Empty),
            new NavItem("Footers", "footers", string.Empty),
            new NavItem("Headers", "headers", string.Empty),
            new NavItem("Grids", "grids", string.Empty),
            new NavItem("Overlays", "overlays", string.Empty),
            new NavItem("Sidebars", "sidebars", string.Empty),
            new NavItem("Tabs", "tabs", string.Empty),
        }),
        new NavHeader("Media", new ISidebarItem[]
        {
            new NavItem("Audio", "audio", string.Empty),
            new NavItem("Images", "images", string.Empty),
            new NavItem("Videos", "videos", string.Empty),
        }),
        new NavHeader("UI", new ISidebarItem[]
        {
            new NavItem("Alerts", "alerts", string.Empty),
            new NavItem("Badges", "badges", string.Empty),
            new NavItem("Breadcrumbs", "breadcrumbs", string.Empty),
            new NavItem("Calendars", "calendars", string.Empty),
            new NavItem("Chips", "chips", string.Empty),
            new NavItem("Code", "code", string.Empty),
            new NavItem("Collapsibles", "collapsibles", string.Empty),
            new NavItem("Icons", "icons", string.Empty),
            new NavItem("Modals", "modals", string.Empty),
            new NavItem("Progress Bars", "progressbars", string.Empty),
            new NavItem("Ratings", "ratings", string.Empty),
            new NavItem("Scroll Bars", "scrollbars", string.Empty),
            new NavItem("Snack Bars", "snackbars", string.Empty),
        }),
        new NavHeader("Visual", new ISidebarItem[]
        {
            new NavItem("Animations", "animations", string.Empty),
            new NavItem("Spinners", "spinners", string.Empty),
        }),
    };
}
