import type { SideObject, Padding, Boundary, RootBoundary, ElementContext, MiddlewareArguments } from './types';
export declare type Options = {
    /**
     * The clipping element(s) in which overflow will be checked.
     */
    boundary: Boundary;
    /**
     * The root clipping element in which overflow will be checked.
     */
    rootBoundary: RootBoundary;
    /**
     * The element in which overflow is being checked relative to a boundary.
     */
    elementContext: ElementContext;
    /**
     * Whether to check for overflow using the alternate element's boundary
     * (`clippingParents` boundary only).
     */
    altBoundary: boolean;
    /**
     * Virtual padding for the resolved overflow offsets.
     */
    padding: Padding;
};
/**
 * Resolves with an object of overflow side offsets that determine how much the
 * element is overflowing a given clipping boundary.
 * - positive = overflowing the boundary by that number of pixels
 * - negative = how many pixels left before it will overflow
 * - 0 = lies flush with the boundary
 */
export declare function detectOverflow(middlewareArguments: MiddlewareArguments, options?: Partial<Options>): Promise<SideObject>;
