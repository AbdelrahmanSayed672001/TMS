$(function () {
	'use strict';
	// Toster Notification         
	$(document).ready(function () {
		setTimeout(function () {
			toastr.options = {
				positionClass: 'toast-top-right',
				closeButton: true,
				progressBar: true,
				showMethod: 'slideDown',
				timeOut: 5000
			};
			toastr.info('Multipurpose Admin Template', 'Hi, welcome to Metrical');

		}, 300);

	});

	// Tickets Count Spark Chart
	window.Apex = {
		stroke: {
			width: 1
		},
		markers: {
			size: 0
		},
		tooltip: {
			fixed: {
				enabled: true,
			}
		}
	};

	var randomizeArray = function (arg) {
		var array = arg.slice();
		var currentIndex = array.length,
			temporaryValue, randomIndex;

		while (0 !== currentIndex) {

			randomIndex = Math.floor(Math.random() * currentIndex);
			currentIndex -= 1;

			temporaryValue = array[currentIndex];
			array[currentIndex] = array[randomIndex];
			array[randomIndex] = temporaryValue;
		}

		return array;
	}

	// data for the sparklines that appear below header area
	var sparklineData = [47, 45, 54, 38, 56, 24, 65, 31, 37, 39, 62, 51, 35, 41, 35, 27, 93, 53, 61, 27, 54, 43, 19, 46];

	/* Count Spark 1 */
	var countSpark1 = {
		chart: {
			type: 'area',
			height: 60,
			fontFamily: 'IBM Plex Sans, sans-serif',
			foreColor: '#6780B1',
			sparkline: {
				enabled: true
			},
		},
		stroke: {
			curve: 'straight'
		},
		fill: {
			opacity: 0.3,
		},
		series: [{
			data: randomizeArray(sparklineData)
		}],
		yaxis: {
			min: 0
		},
		colors: ['#4285F4'],
	}
	/* Count Spark 2 */
	var countSpark2 = {
		chart: {
			type: 'area',
			height: 60,
			fontFamily: 'IBM Plex Sans, sans-serif',
			foreColor: '#6780B1',
			sparkline: {
				enabled: true
			},
		},
		stroke: {
			curve: 'straight'
		},
		fill: {
			opacity: 0.3,
		},
		series: [{
			data: randomizeArray(sparklineData)
		}],
		yaxis: {
			min: 0
		},
		colors: ['#EE8CE5'],
	}
	/* Count Spark 3 */
	var countSpark3 = {
		chart: {
			type: 'area',
			height: 60,
			fontFamily: 'IBM Plex Sans, sans-serif',
			foreColor: '#6780B1',
			sparkline: {
				enabled: true
			},
		},
		stroke: {
			curve: 'straight'
		},
		fill: {
			opacity: 0.3
		},
		series: [{
			data: randomizeArray(sparklineData)
		}],
		xaxis: {
			crosshairs: {
				width: 1
			},
		},
		yaxis: {
			min: 0
		},
		colors: ['#04CAD0'],
	}
	/* Count Spark 4 */
	var countSpark4 = {
		chart: {
			type: 'area',
			height: 60,
			fontFamily: 'IBM Plex Sans, sans-serif',
			foreColor: '#6780B1',
			sparkline: {
				enabled: true
			},
		},
		stroke: {
			curve: 'straight'
		},
		fill: {
			opacity: 0.3
		},
		series: [{
			data: randomizeArray(sparklineData)
		}],
		xaxis: {
			crosshairs: {
				width: 1
			},
		},
		yaxis: {
			min: 0
		},
		colors: ['#F49917'],
	}

	var countSpark1 = new ApexCharts(document.querySelector("#countSpark1"), countSpark1);
	countSpark1.render();
	var countSpark2 = new ApexCharts(document.querySelector("#countSpark2"), countSpark2);
	countSpark2.render();
	var countSpark3 = new ApexCharts(document.querySelector("#countSpark3"), countSpark3);
	countSpark3.render();
	var countSpark4 = new ApexCharts(document.querySelector("#countSpark4"), countSpark4);
	countSpark4.render();


	// Support Ticket Chart
	var options = {
		chart: {
			height: 350,
			type: 'line',
			stacked: false,
			fontFamily: 'IBM Plex Sans, sans-serif',
			foreColor: '#6780B1',
		},
		stroke: {
			width: [1, 2, 3, 5],
			curve: 'smooth'
		},
		plotOptions: {
			bar: {
				columnWidth: '25%',
				endingShape: 'rounded'
			}
		},
		colors: ['#5D78FF', '#C9D5FA', '#63CF72', '#F49917'],
		series: [{
			name: 'New Tickets',
			type: 'column',
			data: [23, 11, 22, 27, 13, 22, 37, 21, 44, 22, 30]
		}, {
			name: 'Solved Tickets',
			type: 'area',
			data: [44, 55, 41, 67, 22, 43, 21, 41, 56, 27, 43]
		}, {
			name: 'Opened Tickets',
			type: 'bar',
			data: [44, 55, 41, 67, 22, 43, 21, 41, 56, 27, 43]
		}, {
			name: 'Unresolved Tickets',
			type: 'line',
			data: [30, 25, 36, 30, 45, 35, 64, 52, 59, 36, 39]
		}],
		fill: {
			opacity: [0.85, 0.25, 1],
			gradient: {
				inverseColors: false,
				shade: 'light',
				type: "vertical",
				opacityFrom: 0.85,
				opacityTo: 0.55,
				stops: [0, 100, 100, 100]
			}
		},
		labels: ['01/01/2003', '02/01/2003', '03/01/2003', '04/01/2003', '05/01/2003', '06/01/2003', '07/01/2003', '08/01/2003', '09/01/2003', '10/01/2003', '11/01/2003'],
		markers: {
			size: 0
		},
		xaxis: {
			type: 'datetime'
		},
		yaxis: {
			min: 0
		},
		tooltip: {
			shared: true,
			intersect: false,
			y: {
				formatter: function (y) {
					if (typeof y !== "undefined") {
						return y.toFixed(0) + "";
					}
					return y;

				}
			}
		},
		legend: {
			labels: {
				useSeriesColors: true
			},
		}
	}

	var chart = new ApexCharts(
		document.querySelector("#supportTicketchart"),
		options
	);

	chart.render();


});