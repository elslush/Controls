using Controls.Colors.CssColors;
using Controls.Selection;
using Controls.Selection.SelectionBehaviors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Sidebars.States
{
    public class NavItemState
    {
        private static readonly NavColors defaultNavItemState = new()
        {
            DefaultColors = new(new BackgroundColor(Color.Transparent), new TextColor(Color.Black)),
            HoverColors = new(new BackgroundColor(Color.FromArgb(94, 164, 228)), new TextColor(Color.White)),
            ClickedColors = new(new BackgroundColor(Color.FromArgb(94, 164, 228)), new TextColor(Color.White)),
        };

        private readonly SelectionCollection<NavItem> navItemCollection = new(new SingleWODeselectBehavior<NavItem>());
        private NavColors navItemColors = defaultNavItemState;

        public NavColors NavItemColors
        {
            get => navItemColors;
            set
            {
                navItemColors = value;
                OnNavItemStateChanged?.Invoke();
            }
        }

        public SelectionCollection<NavItem> NavItemCollection => navItemCollection;

        public event Action? OnNavItemStateChanged;
    }
}
