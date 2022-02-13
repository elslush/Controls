import type { Middleware, Padding } from '../types';
export declare type Options = {
    /**
     * The arrow element to be positioned.
     */
    element: any;
    /**
     * The padding between the arrow element and the floating element edges.
     * Useful when the floating element has rounded corners.
     */
    padding?: Padding;
};
/**
 * Positions an inner element of the floating element such that it is centered
 * to the reference element.
 */
export declare const arrow: (options: Options) => Middleware;
