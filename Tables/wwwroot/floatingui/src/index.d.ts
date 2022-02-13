import { ClientRectObject, ComputePositionConfig } from '@floating-ui/core';
/**
 * Computes the `x` and `y` coordinates that will place the floating element
 * next to a reference element when it is given a certain CSS positioning
 * strategy.
 */
export declare const computePosition: (reference: Element | {
    getBoundingClientRect(): ClientRectObject;
    contextElement?: Element;
}, floating: HTMLElement, options?: Partial<ComputePositionConfig>) => Promise<import("@floating-ui/core").ComputePositionReturn>;
export { arrow, autoPlacement, flip, hide, offset, shift, limitShift, size, inline, detectOverflow, } from '@floating-ui/core';
export { getScrollParents } from './utils/getScrollParents';
