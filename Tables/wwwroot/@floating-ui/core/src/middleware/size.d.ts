import type { Dimensions, ElementRects, Middleware } from '../types';
import { Options as DetectOverflowOptions } from '../detectOverflow';
export declare type Options = {
    /**
     * Function that is called to perform style mutations to the floating element
     * to change its size.
     */
    apply(args: Dimensions & ElementRects): void;
};
/**
 * Provides data to change the size of the floating element. For instance,
 * prevent it from overflowing its clipping boundary or match the width of the
 * reference element.
 */
export declare const size: (options?: Partial<Options & DetectOverflowOptions>) => Middleware;
