function getBasePlacement(placement) {
    return placement.split('-')[0];
}

function getAlignment(placement) {
    return placement.split('-')[1];
}

function getMainAxisFromPlacement(placement) {
    return ['top', 'bottom'].includes(getBasePlacement(placement)) ? 'x' : 'y';
}

function getLengthFromAxis(axis) {
    return axis === 'y' ? 'height' : 'width';
}

function computeCoordsFromPlacement(_ref) {
    let {
        reference,
        floating,
        placement
    } = _ref;
    const commonX = reference.x + reference.width / 2 - floating.width / 2;
    const commonY = reference.y + reference.height / 2 - floating.height / 2;
    let coords;

    switch (getBasePlacement(placement)) {
        case 'top':
            coords = {
                x: commonX,
                y: reference.y - floating.height
            };
            break;

        case 'bottom':
            coords = {
                x: commonX,
                y: reference.y + reference.height
            };
            break;

        case 'right':
            coords = {
                x: reference.x + reference.width,
                y: commonY
            };
            break;

        case 'left':
            coords = {
                x: reference.x - floating.width,
                y: commonY
            };
            break;

        default:
            coords = {
                x: reference.x,
                y: reference.y
            };
    }

    const mainAxis = getMainAxisFromPlacement(placement);
    const length = getLengthFromAxis(mainAxis);

    switch (getAlignment(placement)) {
        case 'start':
            coords[mainAxis] = coords[mainAxis] - (reference[length] / 2 - floating[length] / 2);
            break;

        case 'end':
            coords[mainAxis] = coords[mainAxis] + (reference[length] / 2 - floating[length] / 2);
            break;
    }

    return coords;
}

/**
 * Computes the `x` and `y` coordinates that will place the floating element
 * next to a reference element when it is given a certain positioning strategy.
 *
 * This export does not have any `platform` interface logic. You will need to
 * write one for the platform you are using Floating UI with.
 */

function computePosition$1(reference, floating, config) {
    const {
        placement = 'bottom',
        strategy = 'absolute',
        middleware = [],
        platform
    } = config;

    //if (process.env.NODE_ENV !== "production") {
    //    if (platform == null) {
    //        console.error(['Floating UI: `platform` property was not passed to config. If you', 'want to use Floating UI on the web, install @floating-ui/dom', 'instead of the /core package. Otherwise, you can create your own', '`platform`: https://floating-ui.com/docs/platform'].join(' '));
    //    }

    //    if (middleware.filter(_ref => {
    //        let {
    //            name
    //        } = _ref;
    //        return name === 'autoPlacement' || name === 'flip';
    //    }).length > 1) {
    //        throw new Error(['Floating UI: duplicate `flip` and/or `autoPlacement`', 'middleware detected. This will lead to an infinite loop. Ensure only', 'one of either has been passed to the `middleware` array.'].join(' '));
    //    }
    //}

    let rects = platform.getElementRects({
        reference,
        floating,
        strategy
    });
    let {
        x,
        y
    } = computeCoordsFromPlacement({
        ...rects,
        placement
    });
    let statefulPlacement = placement;
    let middlewareData = {};
    let _debug_loop_count_ = 0;

    for (let i = 0; i < middleware.length; i++) {
        //if (process.env.NODE_ENV !== "production") {
        //    _debug_loop_count_++;

        //    if (_debug_loop_count_ > 100) {
        //        throw new Error(['Floating UI: The middleware lifecycle appears to be', 'running in an infinite loop. This is usually caused by a `reset`', 'continually being returned without a break condition.'].join(' '));
        //    }
        //}

        const {
            name,
            fn
        } = middleware[i];
        const {
            x: nextX,
            y: nextY,
            data,
            reset
        } = fn({
            x,
            y,
            initialPlacement: placement,
            placement: statefulPlacement,
            strategy,
            middlewareData,
            rects,
            platform,
            elements: {
                reference,
                floating
            }
        });
        x = nextX != null ? nextX : x;
        y = nextY != null ? nextY : y;
        middlewareData = {
            ...middlewareData,
            [name]: data != null ? data : {}
        };

        if (reset) {
            if (typeof reset === 'object') {
                if (reset.placement) {
                    statefulPlacement = reset.placement;
                }

                if (reset.rects) {
                    rects = reset.rects === true ? platform.getElementRects({
                        reference,
                        floating,
                        strategy
                    }) : reset.rects;
                }

                ({
                    x,
                    y
                } = computeCoordsFromPlacement({
                    ...rects,
                    placement: statefulPlacement
                }));
            }

            i = -1;
            continue;
        }
    }

    return {
        x,
        y,
        placement: statefulPlacement,
        strategy,
        middlewareData
    };
};

function expandPaddingObject(padding) {
    return {
        top: 0,
        right: 0,
        bottom: 0,
        left: 0,
        ...padding
    };
}

function getSideObjectFromPadding(padding) {
    return typeof padding !== 'number' ? expandPaddingObject(padding) : {
        top: padding,
        right: padding,
        bottom: padding,
        left: padding
    };
}

function rectToClientRect(rect) {
    return {
        ...rect,
        top: rect.y,
        left: rect.x,
        right: rect.x + rect.width,
        bottom: rect.y + rect.height
    };
}

/**
 * Resolves with an object of overflow side offsets that determine how much the
 * element is overflowing a given clipping boundary.
 * - positive = overflowing the boundary by that number of pixels
 * - negative = how many pixels left before it will overflow
 * - 0 = lies flush with the boundary
 */
function detectOverflow(middlewareArguments, options) {
    if (options === void 0) {
        options = {};
    }

    const {
        x,
        y,
        platform,
        rects,
        elements,
        strategy
    } = middlewareArguments;
    const {
        boundary = 'clippingParents',
        rootBoundary = 'viewport',
        elementContext = 'floating',
        altBoundary = false,
        padding = 0
    } = options;
    const paddingObject = getSideObjectFromPadding(padding);
    const altContext = elementContext === 'floating' ? 'reference' : 'floating';
    const element = elements[altBoundary ? altContext : elementContext];
    const clippingClientRect = platform.getClippingClientRect({
        element: (platform.isElement(element)) ? element : element.contextElement || (platform.getDocumentElement({
            element: elements.floating
        })),
        boundary,
        rootBoundary
    });
    const elementClientRect = rectToClientRect(platform.convertOffsetParentRelativeRectToViewportRelativeRect({
        rect: elementContext === 'floating' ? {
            ...rects.floating,
            x,
            y
        } : rects.reference,
        offsetParent: platform.getOffsetParent({
            element: elements.floating
        }),
        strategy
    })); // positive = overflowing the clipping rect
    // 0 or negative = within the clipping rect

    return {
        top: clippingClientRect.top - elementClientRect.top + paddingObject.top,
        bottom: elementClientRect.bottom - clippingClientRect.bottom + paddingObject.bottom,
        left: clippingClientRect.left - elementClientRect.left + paddingObject.left,
        right: elementClientRect.right - clippingClientRect.right + paddingObject.right
    };
}

function within(min$1, value, max$1) {
    return max(min$1, min(value, max$1));
}

/**
 * Positions an inner element of the floating element such that it is centered
 * to the reference element.
 */
const arrow = options => ({
    name: 'arrow',
    options,

    fn(middlewareArguments) {
        // Since `element` is required, we don't Partial<> the type
        const {
            element,
            padding = 0
        } = options != null ? options : {};
        const {
            x,
            y,
            placement,
            rects,
            platform
        } = middlewareArguments;

        if (element == null) {
            //if (process.env.NODE_ENV !== "production") {
            //    console.warn('Floating UI: No `element` was passed to the `arrow` middleware.');
            //}

            return {};
        }

        const paddingObject = getSideObjectFromPadding(padding);
        const coords = {
            x,
            y
        };
        const basePlacement = getBasePlacement(placement);
        const axis = getMainAxisFromPlacement(basePlacement);
        const length = getLengthFromAxis(axis);
        const arrowDimensions = platform.getDimensions({
            element
        });
        const minProp = axis === 'y' ? 'top' : 'left';
        const maxProp = axis === 'y' ? 'bottom' : 'right';
        const endDiff = rects.reference[length] + rects.reference[axis] - coords[axis] - rects.floating[length];
        const startDiff = coords[axis] - rects.reference[axis];
        const arrowOffsetParent = platform.getOffsetParent({
            element
        });
        const clientSize = arrowOffsetParent ? axis === 'y' ? arrowOffsetParent.clientHeight || 0 : arrowOffsetParent.clientWidth || 0 : 0;
        const centerToReference = endDiff / 2 - startDiff / 2; // Make sure the arrow doesn't overflow the floating element if the center
        // point is outside of the floating element's bounds

        const min = paddingObject[minProp];
        const max = clientSize - arrowDimensions[length] - paddingObject[maxProp];
        const center = clientSize / 2 - arrowDimensions[length] / 2 + centerToReference;
        const offset = within(min, center, max);
        return {
            data: {
                [axis]: offset,
                centerOffset: center - offset
            }
        };
    }

});

const hash$1 = {
    left: 'right',
    right: 'left',
    bottom: 'top',
    top: 'bottom'
};
function getOppositePlacement(placement) {
    return placement.replace(/left|right|bottom|top/g, matched => hash$1[matched]);
}

function getAlignmentSides(placement, rects) {
    const isStart = getAlignment(placement) === 'start';
    const mainAxis = getMainAxisFromPlacement(placement);
    const length = getLengthFromAxis(mainAxis);
    let mainAlignmentSide = mainAxis === 'x' ? isStart ? 'right' : 'left' : isStart ? 'bottom' : 'top';

    if (rects.reference[length] > rects.floating[length]) {
        mainAlignmentSide = getOppositePlacement(mainAlignmentSide);
    }

    return {
        main: mainAlignmentSide,
        cross: getOppositePlacement(mainAlignmentSide)
    };
}

const hash = {
    start: 'end',
    end: 'start'
};
function getOppositeAlignmentPlacement(placement) {
    return placement.replace(/start|end/g, matched => hash[matched]);
}

const basePlacements = ['top', 'right', 'bottom', 'left'];
const allPlacements = /*#__PURE__*/basePlacements.reduce((acc, basePlacement) => acc.concat(basePlacement, basePlacement + "-start", basePlacement + "-end"), []);

function getPlacementList(alignment, autoAlignment, allowedPlacements) {
    const allowedPlacementsSortedByAlignment = alignment ? [...allowedPlacements.filter(placement => getAlignment(placement) === alignment), ...allowedPlacements.filter(placement => getAlignment(placement) !== alignment)] : allowedPlacements.filter(placement => getBasePlacement(placement) === placement);
    return allowedPlacementsSortedByAlignment.filter(placement => {
        if (alignment) {
            return getAlignment(placement) === alignment || (autoAlignment ? getOppositeAlignmentPlacement(placement) !== placement : false);
        }

        return true;
    });
}

/**
 * Automatically chooses the `placement` which has the most space available.
 */
const autoPlacement = function (options) {
    if (options === void 0) {
        options = {};
    }

    return {
        name: 'autoPlacement',
        options,

         fn(middlewareArguments) {
            var _middlewareData$autoP, _middlewareData$autoP2, _middlewareData$autoP3, _middlewareData$autoP4, _middlewareData$autoP5, _placementsSortedByLe;

            const {
                x,
                y,
                rects,
                middlewareData,
                placement
            } = middlewareArguments;
            const {
                alignment = null,
                allowedPlacements = allPlacements,
                autoAlignment = true,
                ...detectOverflowOptions
            } = options;

            if ((_middlewareData$autoP = middlewareData.autoPlacement) != null && _middlewareData$autoP.skip) {
                return {};
            }

            const placements = getPlacementList(alignment, autoAlignment, allowedPlacements);
            const overflow = detectOverflow(middlewareArguments, detectOverflowOptions);
            const currentIndex = (_middlewareData$autoP2 = (_middlewareData$autoP3 = middlewareData.autoPlacement) == null ? void 0 : _middlewareData$autoP3.index) != null ? _middlewareData$autoP2 : 0;
            const currentPlacement = placements[currentIndex];
            const {
                main,
                cross
            } = getAlignmentSides(currentPlacement, rects); // Make `computeCoords` start from the right place

            if (placement !== currentPlacement) {
                return {
                    x,
                    y,
                    reset: {
                        placement: placements[0]
                    }
                };
            }

            const currentOverflows = [overflow[getBasePlacement(currentPlacement)], overflow[main], overflow[cross]];
            const allOverflows = [...((_middlewareData$autoP4 = (_middlewareData$autoP5 = middlewareData.autoPlacement) == null ? void 0 : _middlewareData$autoP5.overflows) != null ? _middlewareData$autoP4 : []), {
                placement: currentPlacement,
                overflows: currentOverflows
            }];
            const nextPlacement = placements[currentIndex + 1]; // There are more placements to check

            if (nextPlacement) {
                return {
                    data: {
                        index: currentIndex + 1,
                        overflows: allOverflows
                    },
                    reset: {
                        placement: nextPlacement
                    }
                };
            }

            const placementsSortedByLeastOverflow = allOverflows.slice().sort((a, b) => a.overflows[0] - b.overflows[0]);
            const placementThatFitsOnAllSides = (_placementsSortedByLe = placementsSortedByLeastOverflow.find(_ref => {
                let {
                    overflows
                } = _ref;
                return overflows.every(overflow => overflow <= 0);
            })) == null ? void 0 : _placementsSortedByLe.placement;
            return {
                data: {
                    skip: true
                },
                reset: {
                    placement: placementThatFitsOnAllSides != null ? placementThatFitsOnAllSides : placementsSortedByLeastOverflow[0].placement
                }
            };
        }

    };
};

function getExpandedPlacements(placement) {
    const oppositePlacement = getOppositePlacement(placement);
    return [getOppositeAlignmentPlacement(placement), oppositePlacement, getOppositeAlignmentPlacement(oppositePlacement)];
}

/**
 * Changes the placement of the floating element to one that will fit if the
 * initially specified `placement` does not.
 */
const flip = function (options) {
    if (options === void 0) {
        options = {};
    }

    return {
        name: 'flip',
        options,

         fn(middlewareArguments) {
            var _middlewareData$flip, _middlewareData$flip2;

            const {
                placement,
                middlewareData,
                rects,
                initialPlacement
            } = middlewareArguments;

            if ((_middlewareData$flip = middlewareData.flip) != null && _middlewareData$flip.skip) {
                return {};
            }

            const {
                mainAxis: checkMainAxis = true,
                crossAxis: checkCrossAxis = true,
                fallbackPlacements: specifiedFallbackPlacements,
                fallbackStrategy = 'bestFit',
                flipAlignment = true,
                ...detectOverflowOptions
            } = options;
            const basePlacement = getBasePlacement(placement);
            const isBasePlacement = basePlacement === initialPlacement;
            const fallbackPlacements = specifiedFallbackPlacements || (isBasePlacement || !flipAlignment ? [getOppositePlacement(initialPlacement)] : getExpandedPlacements(initialPlacement));
            const placements = [initialPlacement, ...fallbackPlacements];
            const overflow = detectOverflow(middlewareArguments, detectOverflowOptions);
            const overflows = [];
            let overflowsData = ((_middlewareData$flip2 = middlewareData.flip) == null ? void 0 : _middlewareData$flip2.overflows) || [];

            if (checkMainAxis) {
                overflows.push(overflow[basePlacement]);
            }

            if (checkCrossAxis) {
                const {
                    main,
                    cross
                } = getAlignmentSides(placement, rects);
                overflows.push(overflow[main], overflow[cross]);
            }

            overflowsData = [...overflowsData, {
                placement,
                overflows
            }]; // One or more sides is overflowing

            if (!overflows.every(side => side <= 0)) {
                var _middlewareData$flip$, _middlewareData$flip3;

                const nextIndex = ((_middlewareData$flip$ = (_middlewareData$flip3 = middlewareData.flip) == null ? void 0 : _middlewareData$flip3.index) != null ? _middlewareData$flip$ : 0) + 1;
                const nextPlacement = placements[nextIndex];

                if (nextPlacement) {
                    // Try next placement and re-run the lifecycle
                    return {
                        data: {
                            index: nextIndex,
                            overflows: overflowsData
                        },
                        reset: {
                            placement: nextPlacement
                        }
                    };
                }

                let resetPlacement = 'bottom';

                switch (fallbackStrategy) {
                    case 'bestFit':
                        {
                            var _overflowsData$slice$;

                            const placement = (_overflowsData$slice$ = overflowsData.slice().sort((a, b) => a.overflows.filter(overflow => overflow > 0).reduce((acc, overflow) => acc + overflow, 0) - b.overflows.filter(overflow => overflow > 0).reduce((acc, overflow) => acc + overflow, 0))[0]) == null ? void 0 : _overflowsData$slice$.placement;

                            if (placement) {
                                resetPlacement = placement;
                            }

                            break;
                        }

                    case 'initialPlacement':
                        resetPlacement = initialPlacement;
                        break;
                }

                return {
                    data: {
                        skip: true
                    },
                    reset: {
                        placement: resetPlacement
                    }
                };
            }

            return {};
        }

    };
};

function getSideOffsets(overflow, rect) {
    return {
        top: overflow.top - rect.height,
        right: overflow.right - rect.width,
        bottom: overflow.bottom - rect.height,
        left: overflow.left - rect.width
    };
}

function isAnySideFullyClipped(overflow) {
    return basePlacements.some(side => overflow[side] >= 0);
}
/**
 * Provides data to hide the floating element in applicable situations, such as
 * when it is not in the same clipping context as the reference element.
 */


const hide = () => ({
    name: 'hide',

     fn(modifierArguments) {
        const referenceOverflow = detectOverflow(modifierArguments, {
            elementContext: 'reference'
        });
        const floatingAltOverflow = detectOverflow(modifierArguments, {
            altBoundary: true
        });
        const referenceHiddenOffsets = getSideOffsets(referenceOverflow, modifierArguments.rects.reference);
        const escapedOffsets = getSideOffsets(floatingAltOverflow, modifierArguments.rects.floating);
        const referenceHidden = isAnySideFullyClipped(referenceHiddenOffsets);
        const escaped = isAnySideFullyClipped(escapedOffsets);
        return {
            data: {
                referenceHidden,
                referenceHiddenOffsets,
                escaped,
                escapedOffsets
            }
        };
    }

});

function convertValueToCoords(_ref) {
    let {
        placement,
        rects,
        value
    } = _ref;
    const basePlacement = getBasePlacement(placement);
    const multiplier = ['left', 'top'].includes(basePlacement) ? -1 : 1;
    const rawValue = typeof value === 'function' ? value({
        ...rects,
        placement
    }) : value;
    const {
        mainAxis,
        crossAxis
    } = typeof rawValue === 'number' ? {
        mainAxis: rawValue,
        crossAxis: 0
    } : {
            mainAxis: 0,
            crossAxis: 0,
            ...rawValue
        };
    return getMainAxisFromPlacement(basePlacement) === 'x' ? {
        x: crossAxis,
        y: mainAxis * multiplier
    } : {
        x: mainAxis * multiplier,
        y: crossAxis
    };
}
/**
 * Displaces the floating element from its reference element.
 */

const offset = function (value) {
    if (value === void 0) {
        value = 0;
    }

    return {
        name: 'offset',
        options: value,

        fn(middlewareArguments) {
            const {
                x,
                y,
                placement,
                rects
            } = middlewareArguments;
            const diffCoords = convertValueToCoords({
                placement,
                rects,
                value
            });
            return {
                x: x + diffCoords.x,
                y: y + diffCoords.y,
                data: diffCoords
            };
        }

    };
};

function getCrossAxis(axis) {
    return axis === 'x' ? 'y' : 'x';
}

/**
 * Shifts the floating element in order to keep it in view when it will overflow
 * a clipping boundary.
 */
const shift = function (options) {
    if (options === void 0) {
        options = {};
    }

    return {
        name: 'shift',
        options,

         fn(middlewareArguments) {
            const {
                x,
                y,
                placement
            } = middlewareArguments;
            const {
                mainAxis: checkMainAxis = true,
                crossAxis: checkCrossAxis = false,
                limiter = {
                    fn: _ref => {
                        let {
                            x,
                            y
                        } = _ref;
                        return {
                            x,
                            y
                        };
                    }
                },
                ...detectOverflowOptions
            } = options;
            const coords = {
                x,
                y
            };
            const overflow = detectOverflow(middlewareArguments, detectOverflowOptions);
            const mainAxis = getMainAxisFromPlacement(getBasePlacement(placement));
            const crossAxis = getCrossAxis(mainAxis);
            let mainAxisCoord = coords[mainAxis];
            let crossAxisCoord = coords[crossAxis];

            if (checkMainAxis) {
                const minSide = mainAxis === 'y' ? 'top' : 'left';
                const maxSide = mainAxis === 'y' ? 'bottom' : 'right';
                const min = mainAxisCoord + overflow[minSide];
                const max = mainAxisCoord - overflow[maxSide];
                mainAxisCoord = within(min, mainAxisCoord, max);
            }

            if (checkCrossAxis) {
                const minSide = crossAxis === 'y' ? 'top' : 'left';
                const maxSide = crossAxis === 'y' ? 'bottom' : 'right';
                const min = crossAxisCoord + overflow[minSide];
                const max = crossAxisCoord - overflow[maxSide];
                crossAxisCoord = within(min, crossAxisCoord, max);
            }

            const limitedCoords = limiter.fn({
                ...middlewareArguments,
                [mainAxis]: mainAxisCoord,
                [crossAxis]: crossAxisCoord
            });
            return {
                ...limitedCoords,
                data: {
                    x: limitedCoords.x - x,
                    y: limitedCoords.y - y
                }
            };
        }

    };
};

/**
 * Built-in `limiter` that will stop `shift()` at a certain point.
 */
const limitShift = function (options) {
    if (options === void 0) {
        options = {};
    }

    return {
        options,

        fn(middlewareArguments) {
            const {
                x,
                y,
                placement,
                rects,
                middlewareData
            } = middlewareArguments;
            const {
                offset = 0,
                mainAxis: checkMainAxis = true,
                crossAxis: checkCrossAxis = true
            } = options;
            const coords = {
                x,
                y
            };
            const mainAxis = getMainAxisFromPlacement(placement);
            const crossAxis = getCrossAxis(mainAxis);
            let mainAxisCoord = coords[mainAxis];
            let crossAxisCoord = coords[crossAxis];
            const rawOffset = typeof offset === 'function' ? offset({
                ...rects,
                placement
            }) : offset;
            const computedOffset = typeof rawOffset === 'number' ? {
                mainAxis: rawOffset,
                crossAxis: 0
            } : {
                mainAxis: 0,
                crossAxis: 0,
                ...rawOffset
            };

            if (checkMainAxis) {
                const len = mainAxis === 'y' ? 'height' : 'width';
                const limitMin = rects.reference[mainAxis] - rects.floating[len] + computedOffset.mainAxis;
                const limitMax = rects.reference[mainAxis] + rects.reference[len] - computedOffset.mainAxis;

                if (mainAxisCoord < limitMin) {
                    mainAxisCoord = limitMin;
                } else if (mainAxisCoord > limitMax) {
                    mainAxisCoord = limitMax;
                }
            }

            if (checkCrossAxis) {
                var _middlewareData$offse, _middlewareData$offse2, _middlewareData$offse3, _middlewareData$offse4;

                const len = mainAxis === 'y' ? 'width' : 'height';
                const isOriginSide = ['top', 'left'].includes(getBasePlacement(placement));
                const limitMin = rects.reference[crossAxis] - rects.floating[len] + (isOriginSide ? (_middlewareData$offse = (_middlewareData$offse2 = middlewareData.offset) == null ? void 0 : _middlewareData$offse2[crossAxis]) != null ? _middlewareData$offse : 0 : 0) + (isOriginSide ? 0 : computedOffset.crossAxis);
                const limitMax = rects.reference[crossAxis] + rects.reference[len] + (isOriginSide ? 0 : (_middlewareData$offse3 = (_middlewareData$offse4 = middlewareData.offset) == null ? void 0 : _middlewareData$offse4[crossAxis]) != null ? _middlewareData$offse3 : 0) - (isOriginSide ? computedOffset.crossAxis : 0);

                if (crossAxisCoord < limitMin) {
                    crossAxisCoord = limitMin;
                } else if (crossAxisCoord > limitMax) {
                    crossAxisCoord = limitMax;
                }
            }

            return {
                [mainAxis]: mainAxisCoord,
                [crossAxis]: crossAxisCoord
            };
        }

    };
};

/**
 * Provides data to change the size of the floating element. For instance,
 * prevent it from overflowing its clipping boundary or match the width of the
 * reference element.
 */
const size = function (options) {
    if (options === void 0) {
        options = {};
    }

    return {
        name: 'size',
        options,

         fn(middlewareArguments) {
            var _middlewareData$size;

            const {
                placement,
                rects,
                middlewareData
            } = middlewareArguments;
            const {
                apply,
                ...detectOverflowOptions
            } = options;

            if ((_middlewareData$size = middlewareData.size) != null && _middlewareData$size.skip) {
                return {};
            }

            const overflow = detectOverflow(middlewareArguments, detectOverflowOptions);
            const basePlacement = getBasePlacement(placement);
            const isEnd = getAlignment(placement) === 'end';
            let heightSide;
            let widthSide;

            if (basePlacement === 'top' || basePlacement === 'bottom') {
                heightSide = basePlacement;
                widthSide = isEnd ? 'left' : 'right';
            } else {
                widthSide = basePlacement;
                heightSide = isEnd ? 'top' : 'bottom';
            }

            const xMin = max(overflow.left, 0);
            const xMax = max(overflow.right, 0);
            const yMin = max(overflow.top, 0);
            const yMax = max(overflow.bottom, 0);
            const dimensions = {
                height: rects.floating.height - (['left', 'right'].includes(placement) ? 2 * (yMin !== 0 || yMax !== 0 ? yMin + yMax : max(overflow.top, overflow.bottom)) : overflow[heightSide]),
                width: rects.floating.width - (['top', 'bottom'].includes(placement) ? 2 * (xMin !== 0 || xMax !== 0 ? xMin + xMax : max(overflow.left, overflow.right)) : overflow[widthSide])
            };
            apply == null ? void 0 : apply({
                ...dimensions,
                ...rects
            });
            return {
                data: {
                    skip: true
                },
                reset: {
                    rects: true
                }
            };
        }

    };
};

/**
 * Provides improved positioning for inline reference elements that can span
 * over multiple lines, such as hyperlinks or range selections.
 */
const inline = function (options) {
    if (options === void 0) {
        options = {};
    }

    return {
        name: 'inline',
        options,

         fn(middlewareArguments) {
            var _middlewareData$inlin, _await$platform$getCl;

            const {
                placement,
                elements,
                rects,
                platform,
                strategy,
                middlewareData
            } = middlewareArguments; // A MouseEvent's client{X,Y} coords can be up to 2 pixels off a
            // ClientRect's bounds, despite the event listener being triggered. A
            // padding of 2 seems to handle this issue.

            const {
                padding = 2,
                x,
                y
            } = options;

            if ((_middlewareData$inlin = middlewareData.inline) != null && _middlewareData$inlin.skip) {
                return {};
            }

            const fallback = rectToClientRect(platform.convertOffsetParentRelativeRectToViewportRelativeRect({
                rect: rects.reference,
                offsetParent: platform.getOffsetParent({
                    element: elements.floating
                }),
                strategy
            }));
            const clientRects = Array.from((_await$platform$getCl = (platform.getClientRects == null ? void 0 : platform.getClientRects({
                element: elements.reference
            }))) != null ? _await$platform$getCl : []);
            const paddingObject = getSideObjectFromPadding(padding);

            function getBoundingClientRect() {
                // There are two rects and they are disjoined
                if (clientRects.length === 2 && clientRects[0].left > clientRects[1].right && x != null && y != null) {
                    var _clientRects$find;

                    // Find the first rect in which the point is fully inside
                    return (_clientRects$find = clientRects.find(rect => x > rect.left - paddingObject.left && x < rect.right + paddingObject.right && y > rect.top - paddingObject.top && y < rect.bottom + paddingObject.bottom)) != null ? _clientRects$find : fallback;
                } // There are 2 or more connected rects


                if (clientRects.length >= 2) {
                    if (getMainAxisFromPlacement(placement) === 'x') {
                        const firstRect = clientRects[0];
                        const lastRect = clientRects[clientRects.length - 1];
                        const isTop = getBasePlacement(placement) === 'top';
                        const top = firstRect.top;
                        const bottom = lastRect.bottom;
                        const left = isTop ? firstRect.left : lastRect.left;
                        const right = isTop ? firstRect.right : lastRect.right;
                        const width = right - left;
                        const height = bottom - top;
                        return {
                            top,
                            bottom,
                            left,
                            right,
                            width,
                            height,
                            x: left,
                            y: top
                        };
                    }

                    const isLeftPlacement = getBasePlacement(placement) === 'left';
                    const maxRight = max(...clientRects.map(rect => rect.right));
                    const minLeft = min(...clientRects.map(rect => rect.left));
                    const measureRects = clientRects.filter(rect => isLeftPlacement ? rect.left === minLeft : rect.right === maxRight);
                    const top = measureRects[0].top;
                    const bottom = measureRects[measureRects.length - 1].bottom;
                    const left = minLeft;
                    const right = maxRight;
                    const width = right - left;
                    const height = bottom - top;
                    return {
                        top,
                        bottom,
                        left,
                        right,
                        width,
                        height,
                        x: left,
                        y: top
                    };
                }

                return fallback;
            }

            return {
                data: {
                    skip: true
                },
                reset: {
                    rects: platform.getElementRects({
                        reference: {
                            getBoundingClientRect
                        },
                        floating: elements.floating,
                        strategy
                    })
                }
            };
        }

    };
};

function isWindow(value) {
  return (value == null ? void 0 : value.toString()) === '[object Window]';
}
function getWindow(node) {
  if (node == null) {
    return window;
  }

  if (!isWindow(node)) {
    const ownerDocument = node.ownerDocument;
    return ownerDocument ? ownerDocument.defaultView || window : window;
  }

  return node;
}

function getComputedStyle$1(element) {
  return getWindow(element).getComputedStyle(element);
}

function getNodeName(node) {
  return isWindow(node) ? '' : node ? (node.nodeName || '').toLowerCase() : '';
}

function isHTMLElement(value) {
  return value instanceof getWindow(value).HTMLElement;
}
function isElement(value) {
  return value instanceof getWindow(value).Element;
}
function isNode(value) {
  return value instanceof getWindow(value).Node;
}
function isShadowRoot(node) {
  const OwnElement = getWindow(node).ShadowRoot;
  return node instanceof OwnElement || node instanceof ShadowRoot;
}
function isScrollParent(element) {
  // Firefox wants us to check `-x` and `-y` variations as well
  const {
    overflow,
    overflowX,
    overflowY
  } = getComputedStyle$1(element);
  return /auto|scroll|overlay|hidden/.test(overflow + overflowY + overflowX);
}
function isTableElement(element) {
  return ['table', 'td', 'th'].includes(getNodeName(element));
}
function isContainingBlock(element) {
  // TODO: Try and use feature detection here instead
  const isFirefox = navigator.userAgent.toLowerCase().includes('firefox');
  const css = getComputedStyle$1(element); // This is non-exhaustive but covers the most common CSS properties that
  // create a containing block.
  // https://developer.mozilla.org/en-US/docs/Web/CSS/Containing_block#identifying_the_containing_block

  return css.transform !== 'none' || css.perspective !== 'none' || css.contain === 'paint' || ['transform', 'perspective'].includes(css.willChange) || isFirefox && css.willChange === 'filter' || isFirefox && (css.filter ? css.filter !== 'none' : false);
}

const min = Math.min;
const max = Math.max;
const round = Math.round;

function getBoundingClientRect(element, includeScale) {
  if (includeScale === void 0) {
    includeScale = false;
  }

  const clientRect = element.getBoundingClientRect();
  let scaleX = 1;
  let scaleY = 1;

  if (includeScale && isHTMLElement(element)) {
    scaleX = element.offsetWidth > 0 ? round(clientRect.width) / element.offsetWidth || 1 : 1;
    scaleY = element.offsetHeight > 0 ? round(clientRect.height) / element.offsetHeight || 1 : 1;
  }

  return {
    width: clientRect.width / scaleX,
    height: clientRect.height / scaleY,
    top: clientRect.top / scaleY,
    right: clientRect.right / scaleX,
    bottom: clientRect.bottom / scaleY,
    left: clientRect.left / scaleX,
    x: clientRect.left / scaleX,
    y: clientRect.top / scaleY
  };
}

function getDocumentElement(node) {
  return ((isNode(node) ? node.ownerDocument : node.document) || window.document).documentElement;
}

function getNodeScroll(element) {
  if (isWindow(element)) {
    return {
      scrollLeft: element.pageXOffset,
      scrollTop: element.pageYOffset
    };
  }

  return {
    scrollLeft: element.scrollLeft,
    scrollTop: element.scrollTop
  };
}

function getWindowScrollBarX(element) {
  // If <html> has a CSS width greater than the viewport, then this will be
  // incorrect for RTL.
  return getBoundingClientRect(getDocumentElement(element)).left + getNodeScroll(element).scrollLeft;
}

function isScaled(element) {
  const rect = getBoundingClientRect(element);
  return round(rect.width) !== element.offsetWidth || round(rect.height) !== element.offsetHeight;
}

function getRectRelativeToOffsetParent(element, offsetParent, strategy) {
  const isOffsetParentAnElement = isHTMLElement(offsetParent);
  const documentElement = getDocumentElement(offsetParent);
  const rect = getBoundingClientRect(element, isOffsetParentAnElement && isScaled(offsetParent));
  let scroll = {
    scrollLeft: 0,
    scrollTop: 0
  };
  const offsets = {
    x: 0,
    y: 0
  };

  if (isOffsetParentAnElement || !isOffsetParentAnElement && strategy !== 'fixed') {
    if (getNodeName(offsetParent) !== 'body' || isScrollParent(documentElement)) {
      scroll = getNodeScroll(offsetParent);
    }

    if (isHTMLElement(offsetParent)) {
      const offsetRect = getBoundingClientRect(offsetParent, true);
      offsets.x = offsetRect.x + offsetParent.clientLeft;
      offsets.y = offsetRect.y + offsetParent.clientTop;
    } else if (documentElement) {
      offsets.x = getWindowScrollBarX(documentElement);
    }
  }

  return {
    x: rect.left + scroll.scrollLeft - offsets.x,
    y: rect.top + scroll.scrollTop - offsets.y,
    width: rect.width,
    height: rect.height
  };
}

function getParentNode(node) {
  if (getNodeName(node) === 'html') {
    return node;
  }

  return (// this is a quicker (but less type safe) way to save quite some bytes from the bundle
    // @ts-ignore
    node.assignedSlot || // step into the shadow DOM of the parent of a slotted node
    node.parentNode || ( // DOM Element detected
    isShadowRoot(node) ? node.host : null) || // ShadowRoot detected
    getDocumentElement(node) // fallback

  );
}

function getTrueOffsetParent(element) {
  if (!isHTMLElement(element) || getComputedStyle(element).position === 'fixed') {
    return null;
  }

  return element.offsetParent;
}

function getContainingBlock(element) {
  let currentNode = getParentNode(element);

  while (isHTMLElement(currentNode) && !['html', 'body'].includes(getNodeName(currentNode))) {
    if (isContainingBlock(currentNode)) {
      return currentNode;
    } else {
      currentNode = currentNode.parentNode;
    }
  }

  return null;
} // Gets the closest ancestor positioned element. Handles some edge cases,
// such as table ancestors and cross browser bugs.


function getOffsetParent(element) {
  const window = getWindow(element);
  let offsetParent = getTrueOffsetParent(element);

  while (offsetParent && isTableElement(offsetParent) && getComputedStyle(offsetParent).position === 'static') {
    offsetParent = getTrueOffsetParent(offsetParent);
  }

  if (offsetParent && (getNodeName(offsetParent) === 'html' || getNodeName(offsetParent) === 'body' && getComputedStyle(offsetParent).position === 'static' && !isContainingBlock(offsetParent))) {
    return window;
  }

  return offsetParent || getContainingBlock(element) || window;
}

function getDimensions(element) {
  return {
    width: element.offsetWidth,
    height: element.offsetHeight
  };
}

function convertOffsetParentRelativeRectToViewportRelativeRect(_ref) {
  let {
    rect,
    offsetParent,
    strategy
  } = _ref;
  const isOffsetParentAnElement = isHTMLElement(offsetParent);
  const documentElement = getDocumentElement(offsetParent);

  if (offsetParent === documentElement) {
    return rect;
  }

  let scroll = {
    scrollLeft: 0,
    scrollTop: 0
  };
  const offsets = {
    x: 0,
    y: 0
  };

  if (isOffsetParentAnElement || !isOffsetParentAnElement && strategy !== 'fixed') {
    if (getNodeName(offsetParent) !== 'body' || isScrollParent(documentElement)) {
      scroll = getNodeScroll(offsetParent);
    }

    if (isHTMLElement(offsetParent)) {
      const offsetRect = getBoundingClientRect(offsetParent, true);
      offsets.x = offsetRect.x + offsetParent.clientLeft;
      offsets.y = offsetRect.y + offsetParent.clientTop;
    } // This doesn't appear to be need to be negated.
    // else if (documentElement) {
    //   offsets.x = getWindowScrollBarX(documentElement);
    // }

  }

  return { ...rect,
    x: rect.x - scroll.scrollLeft + offsets.x,
    y: rect.y - scroll.scrollTop + offsets.y
  };
}

function getViewportRect(element) {
  const win = getWindow(element);
  const html = getDocumentElement(element);
  const visualViewport = win.visualViewport;
  let width = html.clientWidth;
  let height = html.clientHeight;
  let x = 0;
  let y = 0;

  if (visualViewport) {
    width = visualViewport.width;
    height = visualViewport.height; // Uses Layout Viewport (like Chrome; Safari does not currently)
    // In Chrome, it returns a value very close to 0 (+/-) but contains rounding
    // errors due to floating point numbers, so we need to check precision.
    // Safari returns a number <= 0, usually < -1 when pinch-zoomed

    if (Math.abs(win.innerWidth / visualViewport.scale - visualViewport.width) < 0.01) {
      x = visualViewport.offsetLeft;
      y = visualViewport.offsetTop;
    }
  }

  return {
    width,
    height,
    x,
    y
  };
}

// of the `<html>` and `<body>` rect bounds if horizontally scrollable

function getDocumentRect(element) {
  var _element$ownerDocumen;

  const html = getDocumentElement(element);
  const scroll = getNodeScroll(element);
  const body = (_element$ownerDocumen = element.ownerDocument) == null ? void 0 : _element$ownerDocumen.body;
  const width = max(html.scrollWidth, html.clientWidth, body ? body.scrollWidth : 0, body ? body.clientWidth : 0);
  const height = max(html.scrollHeight, html.clientHeight, body ? body.scrollHeight : 0, body ? body.clientHeight : 0);
  let x = -scroll.scrollLeft + getWindowScrollBarX(element);
  const y = -scroll.scrollTop;

  if (getComputedStyle$1(body || html).direction === 'rtl') {
    x += max(html.clientWidth, body ? body.clientWidth : 0) - width;
  }

  return {
    width,
    height,
    x,
    y
  };
}

function getScrollParent(node) {
  if (['html', 'body', '#document'].includes(getNodeName(node))) {
    // @ts-ignore assume body is always available
    return node.ownerDocument.body;
  }

  if (isHTMLElement(node) && isScrollParent(node)) {
    return node;
  }

  return getScrollParent(getParentNode(node));
}

function getScrollParents(node, list) {
  var _node$ownerDocument;

  if (list === void 0) {
    list = [];
  }

  const scrollParent = getScrollParent(node);
  const isBody = scrollParent === ((_node$ownerDocument = node.ownerDocument) == null ? void 0 : _node$ownerDocument.body);
  const win = getWindow(scrollParent);
  const target = isBody ? [win].concat(win.visualViewport || [], isScrollParent(scrollParent) ? scrollParent : []) : scrollParent;
  const updatedList = list.concat(target);
  return isBody ? updatedList : // @ts-ignore: isBody tells us target will be an HTMLElement here
  updatedList.concat(getScrollParents(getParentNode(target)));
}

function contains(parent, child) {
  const rootNode = child.getRootNode == null ? void 0 : child.getRootNode(); // First, attempt with faster native method

  if (parent.contains(child)) {
    return true;
  } // then fallback to custom implementation with Shadow DOM support
  else if (rootNode && isShadowRoot(rootNode)) {
    let next = child;

    do {
      // use `===` replace node.isSameNode()
      if (next && parent === next) {
        return true;
      } // @ts-ignore: need a better way to handle this...


      next = next.parentNode || next.host;
    } while (next);
  }

  return false;
}

function getInnerBoundingClientRect(element) {
  const clientRect = getBoundingClientRect(element);
  const top = clientRect.top + element.clientTop;
  const left = clientRect.left + element.clientLeft;
  return {
    top,
    left,
    x: left,
    y: top,
    right: left + element.clientWidth,
    bottom: top + element.clientHeight,
    width: element.clientWidth,
    height: element.clientHeight
  };
}

function getClientRectFromClippingParent(element, clippingParent) {
  if (clippingParent === 'viewport') {
    return rectToClientRect(getViewportRect(element));
  }

  if (isElement(clippingParent)) {
    return getInnerBoundingClientRect(clippingParent);
  }

  return rectToClientRect(getDocumentRect(getDocumentElement(element)));
} // A "clipping parent" is an overflowable container with the characteristic of
// clipping (or hiding) overflowing elements with a position different from
// `initial`


function getClippingParents(element) {
  const clippingParents = getScrollParents(getParentNode(element));
  const canEscapeClipping = ['absolute', 'fixed'].includes(getComputedStyle$1(element).position);
  const clipperElement = canEscapeClipping && isHTMLElement(element) ? getOffsetParent(element) : element;

  if (!isElement(clipperElement)) {
    return [];
  } // @ts-ignore isElement check ensures we return Array<Element>


  return clippingParents.filter(clippingParent => isElement(clippingParent) && contains(clippingParent, clipperElement) && getNodeName(clippingParent) !== 'body');
} // Gets the maximum area that the element is visible in due to any number of
// clipping parents


function getClippingClientRect(_ref) {
  let {
    element,
    boundary,
    rootBoundary
  } = _ref;
  const mainClippingParents = boundary === 'clippingParents' ? getClippingParents(element) : [].concat(boundary);
  const clippingParents = [...mainClippingParents, rootBoundary];
  const firstClippingParent = clippingParents[0];
  const clippingRect = clippingParents.reduce((accRect, clippingParent) => {
    const rect = getClientRectFromClippingParent(element, clippingParent);
    accRect.top = max(rect.top, accRect.top);
    accRect.right = min(rect.right, accRect.right);
    accRect.bottom = min(rect.bottom, accRect.bottom);
    accRect.left = max(rect.left, accRect.left);
    return accRect;
  }, getClientRectFromClippingParent(element, firstClippingParent));
  clippingRect.width = clippingRect.right - clippingRect.left;
  clippingRect.height = clippingRect.bottom - clippingRect.top;
  clippingRect.x = clippingRect.left;
  clippingRect.y = clippingRect.top;
  return clippingRect;
}

const platform = {
  getElementRects: _ref => {
    let {
      reference,
      floating,
      strategy
    } = _ref;
    return {
      reference: getRectRelativeToOffsetParent(reference, getOffsetParent(floating), strategy),
      floating: { ...getDimensions(floating),
        x: 0,
        y: 0
      }
    };
  },
  convertOffsetParentRelativeRectToViewportRelativeRect: args => convertOffsetParentRelativeRectToViewportRelativeRect(args),
  getOffsetParent: _ref2 => {
    let {
      element
    } = _ref2;
    return getOffsetParent(element);
  },
  isElement: value => isElement(value),
  getDocumentElement: _ref3 => {
    let {
      element
    } = _ref3;
    return getDocumentElement(element);
  },
  getClippingClientRect: args => getClippingClientRect(args),
  getDimensions: _ref4 => {
    let {
      element
    } = _ref4;
    return getDimensions(element);
  },
  getClientRects: _ref5 => {
    let {
      element
    } = _ref5;
    return element.getClientRects();
  }
};

/**
 * Computes the `x` and `y` coordinates that will place the floating element
 * next to a reference element when it is given a certain CSS positioning
 * strategy.
 */

function computePosition(reference, floating, options) {
    return computePosition$1(reference, floating, {
        platform,
        ...options
    });
}

export { arrow, autoPlacement, computePosition, getScrollParents, detectOverflow, flip, hide, inline, limitShift, offset, shift, size };