export function scrollTo(e) {
    e.scrollIntoView();
    if (e.id) {
        window.location.hash = e.id;
    }
}