/**
 * @license Highstock JS v11.0.1 (2023-05-08)
 * @module highcharts/modules/drag-panes
 * @requires highcharts
 * @requires highcharts/modules/stock
 *
 * Drag-panes module
 *
 * (c) 2010-2021 Highsoft AS
 * Author: Kacper Madej
 *
 * License: www.highcharts.com/license
 */
'use strict';
import Highcharts from '../../Core/Globals.js';
import AxisResizer from '../../Extensions/DragPanes/AxisResizer.js';
import DragPanes from '../../Extensions/DragPanes/DragPanes.js';
const G = Highcharts;
G.AxisResizer = AxisResizer;
DragPanes.compose(G.Axis, G.Pointer);
