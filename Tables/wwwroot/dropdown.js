export function addCloseEventListeners(elem) {
    const bodyClickHandler = event => {
        if (event.path.indexOf(elem) < 0) {
            elem.dispatchEvent(new CustomEvent('closecolumnoptions', { bubbles: true }));
        }
    };

    document.body.addEventListener('click', bodyClickHandler);
    document.body.addEventListener('mousedown', bodyClickHandler);

    return {
        stop: () => {
            document.body.removeEventListener('click', bodyClickHandler);
            document.body.removeEventListener('mousedown', bodyClickHandler);
        }
    };
}