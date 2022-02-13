export function copy(text) {
    if (!navigator.clipboard) {
        var textArea = document.createElement("textarea");
        textArea.value = text;
        document.body.appendChild(textArea);
        textArea.focus();
        textArea.select();

        try {
            document.execCommand("copy");
        } catch (err) { }

        document.body.removeChild(textArea);
    }
    else {
        await navigator.clipboard.writeText(text);
    }
}