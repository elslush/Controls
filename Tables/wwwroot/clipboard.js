export function copy(t) {
    if (!navigator.clipboard) {
        var ta = document.createElement("textarea");
        ta.value = t;
        document.body.appendChild(ta);
        ta.focus();
        ta.select();

        try {
            document.execCommand("copy");
        } catch (err) { }

        document.body.removeChild(ta);
    }
    else {
        await navigator.clipboard.writeText(t);
    }
}