import type { Placement, Middleware } from '../types';
import { Options as DetectOverflowOptions } from '../detectOverflow';
export declare type Options = {
    /**
     * The axis that runs along the side of the floating element.
     */
    mainAxis: boolean;
    /**
     * The axis that runs along the alignment of the floating element.
     */
    crossAxis: boolean;
    /**
     * Placements to try if the preferred `placement` does not fit.
     */
    fallbackPlacements: Array<Placement>;
    /**
     * What strategy to use when no placements fit.
     */
    fallbackStrategy: 'bestFit' | 'initialPlacement';
    /**
     * Whether to flip to placements with the opposite alignment if they fit
     * better.
     */
    flipAlignment: boolean;
};
/**
 * Changes the placement of the floating element to one that will fit if the
 * initially specified `placement` does not.
 */
export declare const flip: (options?: Partial<Options & DetectOverflowOptions>) => Middleware;
