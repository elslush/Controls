import { computePosition } from '@floating-ui/dom';

export function addToolTip(button, tooltip) {
    computePosition(button, tooltip).then(({ x, y }) => {
        Object.assign(tooltip.style, {
            left: `${x}px`,
            top: `${y}px`,
        });
    });
}