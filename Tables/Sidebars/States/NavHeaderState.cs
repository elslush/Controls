using Controls.Colors.CssColors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Sidebars.States
{
    public class NavHeaderState
    {
        private static readonly NavColors defaultNavHeaderColors = new()
        {
            DefaultColors = new(new BackgroundColor(Color.Transparent), new TextColor(Color.Black)),
            HoverColors = new(new BackgroundColor(Color.FromArgb(94, 164, 228)), new TextColor(Color.White)),
            ClickedColors = new(new BackgroundColor(Color.FromArgb(94, 164, 228)), new TextColor(Color.White)),
        };

        private NavColors navHeaderColors = defaultNavHeaderColors;

        public NavColors NavItemColors
        {
            get => navHeaderColors;
            set
            {
                navHeaderColors = value;
                OnNavHeaderStateChanged?.Invoke();
            }
        }

        public event Action? OnNavHeaderStateChanged;
    }
}
