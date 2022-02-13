import type { Middleware } from '../types';
/**
 * Provides data to hide the floating element in applicable situations, such as
 * when it is not in the same clipping context as the reference element.
 */
export declare const hide: () => Middleware;
