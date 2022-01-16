////import "/_content/Controls/highlight.min.js"
//import hljs from '/_content/Controls/highlight.min.js';
import hljs from 'https://unpkg.com/@highlightjs/cdn-assets@11.4.0/es/highlight.min.js'
import csharp from 'https://unpkg.com/@highlightjs/cdn-assets@11.4.0/es/languages/csharp.min.js'
//import javascript from 'highlight.js/lib/languages/javascript';
//hljs.registerLanguage('javascript', javascript);

export async function importLanguage(l) {
    //const csharp = await import('https://unpkg.com/@highlightjs/cdn-assets@11.4.0/es/languages/csharp.min.js');
    //let module = await import('/modules/my-module.js');
    hljs.registerLanguage('csharp', csharp);
}

export function highlightSnippet(el) {
    //hljs.highlightElement(el);
    hljs.highlightAll();
}