import type { Coords, Middleware, Padding } from '../types';
export declare type Options = Coords & {
    /**
     * @experimental
     */
    padding: Padding;
};
/**
 * Provides improved positioning for inline reference elements that can span
 * over multiple lines, such as hyperlinks or range selections.
 */
export declare const inline: (options?: Partial<Options>) => Middleware;
