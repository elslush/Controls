export function addWindowClickEventListener(dotNetObjectRef) {
    window.addEventListener('click', (event) => {
        dotNetObjectRef.invokeMethodAsync('ClickWindow');
    });
}