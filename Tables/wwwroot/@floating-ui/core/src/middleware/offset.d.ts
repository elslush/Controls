import type { Placement, Rect, Coords, Middleware } from '../types';
declare type OffsetValue = number | {
    /**
     * The axis that runs along the side of the floating element.
     */
    mainAxis?: number;
    /**
     * The axis that runs along the alignment of the floating element.
     */
    crossAxis?: number;
};
declare type OffsetFunction = (args: {
    floating: Rect;
    reference: Rect;
    placement: Placement;
}) => OffsetValue;
export declare type Options = OffsetValue | OffsetFunction;
export declare function convertValueToCoords({ placement, rects, value, }: {
    placement: Placement;
    rects: {
        floating: Rect;
        reference: Rect;
    };
    value: Options;
}): Coords;
/**
 * Displaces the floating element from its reference element.
 */
export declare const offset: (value?: Options) => Middleware;
export {};
