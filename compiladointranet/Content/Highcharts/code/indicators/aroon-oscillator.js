/*
 Highstock JS v11.0.1 (2023-05-08)

 Indicator series type for Highcharts Stock

 (c) 2010-2021 Wojciech Chmiel

 License: www.highcharts.com/license
*/
'use strict';(function(a){"object"===typeof module&&module.exports?(a["default"]=a,module.exports=a):"function"===typeof define&&define.amd?define("highcharts/indicators/aroon-oscillator",["highcharts","highcharts/modules/stock"],function(e){a(e);a.Highcharts=e;return a}):a("undefined"!==typeof Highcharts?Highcharts:void 0)})(function(a){function e(a,c,f,u){a.hasOwnProperty(c)||(a[c]=u.apply(null,f),"function"===typeof CustomEvent&&window.dispatchEvent(new CustomEvent("HighchartsModuleLoaded",{detail:{path:c,
module:a[c]}})))}a=a?a._modules:{};e(a,"Stock/Indicators/MultipleLinesComposition.js",[a["Core/Series/SeriesRegistry.js"],a["Core/Utilities.js"]],function(a,c){const {sma:{prototype:f}}=a.seriesTypes,{defined:u,error:e,merge:t}=c;var g;(function(a){function x(b){return"plot"+b.charAt(0).toUpperCase()+b.slice(1)}function g(b,h){const a=[];(b.pointArrayMap||[]).forEach(b=>{b!==h&&a.push(x(b))});return a}function w(){const b=this,a=b.linesApiNames;var d=b.areaLinesNames;const m=b.points,c=b.options,
w=b.graph,l={options:{gapSize:c.gapSize}},n=[];var p=g(b,b.pointValKey);let r=m.length,q;p.forEach((b,a)=>{for(n[a]=[];r--;)q=m[r],n[a].push({x:q.x,plotX:q.plotX,plotY:q[b],isNull:!u(q[b])});r=m.length});if(b.userOptions.fillColor&&d.length){var k=p.indexOf(x(d[0]));k=n[k];d=1===d.length?m:n[p.indexOf(x(d[1]))];p=b.color;b.points=d;b.nextPoints=k;b.color=b.userOptions.fillColor;b.options=t(m,l);b.graph=b.area;b.fillGraph=!0;f.drawGraph.call(b);b.area=b.graph;delete b.nextPoints;delete b.fillGraph;
b.color=p}a.forEach((a,h)=>{n[h]?(b.points=n[h],c[a]?b.options=t(c[a].styles,l):e('Error: "There is no '+a+' in DOCS options declared. Check if linesApiNames are consistent with your DOCS line names."'),b.graph=b["graph"+a],f.drawGraph.call(b),b["graph"+a]=b.graph):e('Error: "'+a+" doesn't have equivalent in pointArrayMap. To many elements in linesApiNames relative to pointArrayMap.\"")});b.points=m;b.options=c;b.graph=w;f.drawGraph.call(b)}function r(b){var a;let d=[];b=b||this.points;if(this.fillGraph&&
this.nextPoints){if((a=f.getGraphPath.call(this,this.nextPoints))&&a.length){a[0][0]="L";d=f.getGraphPath.call(this,b);a=a.slice(0,d.length);for(let b=a.length-1;0<=b;b--)d.push(a[b])}}else d=f.getGraphPath.apply(this,arguments);return d}function k(b){const a=[];(this.pointArrayMap||[]).forEach(h=>{a.push(b[h])});return a}function l(){const b=this.pointArrayMap;let a=[],d;a=g(this);f.translate.apply(this,arguments);this.points.forEach(h=>{b.forEach((b,c)=>{d=h[b];this.dataModify&&(d=this.dataModify.modifyValue(d));
null!==d&&(h[a[c]]=this.yAxis.toPixels(d,!0))})})}const y=[],v=["bottomLine"],z=["top","bottom"],A=["top"];a.compose=function(b){if(c.pushUnique(y,b)){const a=b.prototype;a.linesApiNames=a.linesApiNames||v.slice();a.pointArrayMap=a.pointArrayMap||z.slice();a.pointValKey=a.pointValKey||"top";a.areaLinesNames=a.areaLinesNames||A.slice();a.drawGraph=w;a.getGraphPath=r;a.toYData=k;a.translate=l}return b}})(g||(g={}));return g});e(a,"Stock/Indicators/AroonOscillator/AroonOscillatorIndicator.js",[a["Stock/Indicators/MultipleLinesComposition.js"],
a["Core/Series/SeriesRegistry.js"],a["Core/Utilities.js"]],function(a,c,f){const {aroon:e}=c.seriesTypes,{extend:v,merge:t}=f;class g extends e{constructor(){super(...arguments);this.points=this.options=this.data=void 0}getValues(a,c){const e=[],f=[],g=[];let k;c=super.getValues.call(this,a,c);for(a=0;a<c.yData.length;a++){var l=c.yData[a][0];k=c.yData[a][1];l-=k;e.push([c.xData[a],l]);f.push(c.xData[a]);g.push(l)}return{values:e,xData:f,yData:g}}}g.defaultOptions=t(e.defaultOptions,{tooltip:{pointFormat:'<span style="color:{point.color}">\u25cf</span><b> {series.name}</b>: {point.y}'}});
v(g.prototype,{nameBase:"Aroon Oscillator",linesApiNames:[],pointArrayMap:["y"],pointValKey:"y"});a.compose(e);c.registerSeriesType("aroonoscillator",g);"";return g});e(a,"masters/indicators/aroon-oscillator.src.js",[],function(){})});
//# sourceMappingURL=aroon-oscillator.js.map