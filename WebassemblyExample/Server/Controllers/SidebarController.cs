using Controls.CssStates;
using Controls.Sidebars;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace WebassemblyExample.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SidebarController : ControllerBase
    {
        private static readonly NavColors navColors = new()
        {
            DefaultColors = new(new CssColor("background-color", Color.Transparent), new CssColor("color", Color.Black)),
            HoverColors = new(new CssColor("background-color", Color.FromArgb(94, 164, 228)), new CssColor("color", Color.White)),
            ClickedColors = new(new CssColor("background-color", Color.FromArgb(94, 164, 228)), new CssColor("color", Color.White)),
        };
        private static readonly NavColors headerColors = new()
        {
            DefaultColors = new(new CssColor("background-color", Color.Transparent), new CssColor("color", Color.Black)),
            HoverColors = new(new CssColor("color", Color.FromArgb(0, 158, 247))),
            ClickedColors = new(new CssColor("color", Color.FromArgb(0, 158, 247))),
        };
        private static readonly ISidebarItem[] items = new ISidebarItem[]
        {
            new NavItem("Cookies", "cookies", string.Empty, navColors),
            new NavHeader("Data", headerColors, new ISidebarItem[]
            {
                new NavItem("Tables", "tables", string.Empty, navColors),
                new NavItem("Filters", "filters", string.Empty, navColors),
                new NavItem("Pagination", "pagination", string.Empty, navColors),
                new NavItem("Selection", "selection", string.Empty, navColors),
                new NavItem("Sorting", "sorting", string.Empty, navColors),
            }),
            new NavHeader("Inputs", headerColors, new ISidebarItem[]
            {
                new NavItem("Buttons", "buttons", string.Empty, navColors),
                new NavItem("Captchas", "captchas", string.Empty, navColors),
                new NavItem("Check Boxes", "checkboxes", string.Empty, navColors),
                new NavItem("Clipboard", "clipboard", string.Empty, navColors),
                new NavItem("Color Pickers", "colorpickers", string.Empty, navColors),
                new NavItem("Date Pickers", "datepickers", string.Empty, navColors),
                new NavItem("Draggables", "draggables", string.Empty, navColors),
                new NavItem("Dropdowns", "dropdowns", string.Empty, navColors),
                new NavItem("File Drops", "filedrops", string.Empty, navColors),
                new NavItem("Text", "text", string.Empty, navColors),
                new NavItem("Sliders", "sliders", string.Empty, navColors),
                //new NavItem("Text Editors", "editors", string.Empty, navColors),
            }),
            new NavHeader("Layout", headerColors, new ISidebarItem[]
            {
                new NavItem("Containers", "containers", string.Empty, navColors),
                new NavItem("Footers", "footers", string.Empty, navColors),
                new NavItem("Headers", "headers", string.Empty, navColors),
                new NavItem("Grids", "grids", string.Empty, navColors),
                new NavItem("Overlays", "overlays", string.Empty, navColors),
                new NavItem("Sidebars", "sidebars", string.Empty, navColors),
                new NavItem("Tabs", "tabs", string.Empty, navColors),
            }),
            new NavHeader("Media", headerColors, new ISidebarItem[]
            {
                new NavItem("Audio", "audio", string.Empty, navColors),
                new NavItem("Images", "images", string.Empty, navColors),
                new NavItem("Videos", "videos", string.Empty, navColors),
            }),
            new NavHeader("UI", headerColors, new ISidebarItem[]
            {
                new NavItem("Alerts", "alerts", string.Empty, navColors),
                new NavItem("Badges", "badges", string.Empty, navColors),
                new NavItem("Breadcrumbs", "breadcrumbs", string.Empty, navColors),
                new NavItem("Calendars", "calendars", string.Empty, navColors),
                new NavItem("Chips", "chips", string.Empty, navColors),
                new NavItem("Code", "code", string.Empty, navColors),
                new NavItem("Collapsibles", "collapsibles", string.Empty, navColors),
                new NavItem("Icons", "icons", string.Empty, navColors),
                new NavItem("Modals", "modals", string.Empty, navColors),
                new NavItem("Progress Bars", "progressbars", string.Empty, navColors),
                new NavItem("Ratings", "ratings", string.Empty, navColors),
                new NavItem("Scroll Bars", "scrollbars", string.Empty, navColors),
                new NavItem("Snack Bars", "snackbars", string.Empty, navColors),
            }),
            new NavHeader("Visual", headerColors, new ISidebarItem[]
            {
                new NavItem("Animations", "animations", string.Empty, navColors),
                new NavItem("Spinners", "spinners", string.Empty, navColors),
            }),
        };

        [HttpGet("SidebarItems")]
        public IEnumerable<ISidebarItem> GetSidebarItems() => items;
    }
}
