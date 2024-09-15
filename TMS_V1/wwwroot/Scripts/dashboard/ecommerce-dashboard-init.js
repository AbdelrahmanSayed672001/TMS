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


	// Count Chart 1
	var countChart1 = {
		chart: {
			type: 'bar',
			height: 50,
			fontFamily: 'IBM Plex Sans, sans-serif',
			foreColor: '#6780B1',
			sparkline: {
				enabled: true
			}
		},
		plotOptions: {
			bar: {
				columnWidth: '25%'
			}
		},
		series: [{
			data: [25, 66, 41, 89, 63, 25, 44, 12, 36, 9, 54, 25, 66, 41, 89, 63, 25, 44, 12, 36, 9, 54, 25, 66, 41, 89, 63, 25, 44, 12, 36, 9, 54, 25, 66, 41, 89, 63, 25, 44, 12, 36, 9, 54]
		}],
		labels: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11],
		xaxis: {
			crosshairs: {
				width: 1
			},
		},
		colors: ['#F73127'],
		tooltip: {
			fixed: {
				enabled: false
			},
			x: {
				show: false
			},
			y: {
				title: {
					formatter: function (seriesName) {
						return ''
					}
				}
			},
			marker: {
				show: false
			}
		}
	}

	new ApexCharts(document.querySelector("#countChart1"), countChart1).render();

	// Count Chart 2
	var countChart2 = {
		chart: {
			type: 'bar',
			height: 50,
			fontFamily: 'IBM Plex Sans, sans-serif',
			foreColor: '#6780B1',
			sparkline: {
				enabled: true
			}
		},
		plotOptions: {
			bar: {
				columnWidth: '25%'
			}
		},
		series: [{
			data: [25, 66, 41, 89, 63, 25, 44, 12, 36, 9, 54, 25, 66, 41, 89, 63, 25, 44, 12, 36, 9, 54, 25, 66, 41, 89, 63, 25, 44, 12, 36, 9, 54, 25, 66, 41, 89, 63, 25, 44, 12, 36, 9, 54]
		}],
		labels: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11],
		xaxis: {
			crosshairs: {
				width: 1
			},
		},
		colors: ['#63CF72'],
		tooltip: {
			fixed: {
				enabled: false
			},
			x: {
				show: false
			},
			y: {
				title: {
					formatter: function (seriesName) {
						return ''
					}
				}
			},
			marker: {
				show: false
			}
		}
	}

	new ApexCharts(document.querySelector("#countChart2"), countChart2).render();

	// Count Chart 3
	var countChart3 = {
		chart: {
			type: 'bar',
			height: 50,
			fontFamily: 'IBM Plex Sans, sans-serif',
			foreColor: '#6780B1',
			sparkline: {
				enabled: true
			}
		},
		plotOptions: {
			bar: {
				columnWidth: '25%'
			}
		},
		series: [{
			data: [25, 66, 41, 89, 63, 25, 44, 12, 36, 9, 54, 25, 66, 41, 89, 63, 25, 44, 12, 36, 9, 54, 25, 66, 41, 89, 63, 25, 44, 12, 36, 9, 54, 25, 66, 41, 89, 63, 25, 44, 12, 36, 9, 54]
		}],
		labels: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11],
		xaxis: {
			crosshairs: {
				width: 1
			},
		},
		colors: ['#5D78FF'],
		tooltip: {
			fixed: {
				enabled: false
			},
			x: {
				show: false
			},
			y: {
				title: {
					formatter: function (seriesName) {
						return ''
					}
				}
			},
			marker: {
				show: false
			}
		}
	}

	new ApexCharts(document.querySelector("#countChart3"), countChart3).render();


	// Monthly Report Chart
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
		colors: ['#5D78FF', '#F49917', '#63CF72', '#C9D5FA'],
		series: [{
			name: 'New Order',
			type: 'column',
			data: [23, 11, 22, 27, 13, 22, 37, 21, 44, 22, 30]
		}, {
			name: 'Total Orders',
			type: 'line',
			data: [44, 55, 41, 67, 22, 43, 21, 41, 56, 27, 43]
		}, {
			name: 'Total Sale',
			type: 'bar',
			data: [44, 55, 41, 67, 22, 43, 21, 41, 56, 27, 43]
		}, {
			name: 'Total Income',
			type: 'area',
			data: [30, 25, 36, 30, 45, 35, 44, 22, 29, 16, 19]
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