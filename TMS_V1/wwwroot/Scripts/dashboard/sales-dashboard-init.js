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

	// AnnualReport Chart
	var ctx6 = document.getElementById('annualReport');
	new Chart(ctx6, {
		type: 'line',
		data: {
			labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'],
			datasets: [{
				label: 'Sales Report',
				data: [5, 15, 5, 20, 15, 25],
				backgroundColor: 'rgba(0, 204, 204, .2)',
				borderWidth: 1,
				fill: true
			}, {
				label: 'Annual Revenue',
				data: [10, 10, 15, 5, 20, 15],
				backgroundColor: 'rgba(248, 127, 186, .2)',
				borderWidth: 1,
				fill: true
			}, {
				label: 'Total Profit',
				data: [15, 5, 15, 10, 25, 20],
				backgroundColor: 'rgba(152, 194, 252, .2)',
				borderWidth: 1,
				fill: true
			}]
		},
		options: {
			legend: {
				display: true,
				labels: {
					display: true,
					fontFamily: 'IBM Plex Sans, sans-serif',
					fontColor: '#6780B1',
				}
			},
			scales: {
				yAxes: [{
					ticks: {
						beginAtZero: true,
						fontSize: 11,
						fontFamily: 'IBM Plex Sans, sans-serif',
						fontColor: '#6780B1',
						max: 25
					}
				}],
				xAxes: [{
					ticks: {
						beginAtZero: true,
						fontSize: 11,
						fontFamily: 'IBM Plex Sans, sans-serif',
						fontColor: '#6780B1',
					}
				}]
			}
		}
	});


	// Sales+Order+Revenue Slider Chart
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


	/* Sales Spark Chart 1 */
	var salesSpark1 = {
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
		colors: ['#04CAD0'],
	}
	/* Sales Spark Chart 2 */
	var salesSpark2 = {
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
	/* Sales Spark Chart 3 */
	var salesSpark3 = {
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
		colors: ['#EE8CE5'],
	}

	var salesSpark1 = new ApexCharts(document.querySelector("#salesSpark1"), salesSpark1);
	salesSpark1.render();
	var salesSpark2 = new ApexCharts(document.querySelector("#salesSpark2"), salesSpark2);
	salesSpark2.render();
	var salesSpark3 = new ApexCharts(document.querySelector("#salesSpark3"), salesSpark3);
	salesSpark3.render();


	/* Order Spark Chart 1 */
	var orderSpark1 = {
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
	/* Order Spark Chart 2 */
	var orderSpark2 = {
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
	/* Order Spark Chart 3 */
	var orderSpark3 = {
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

	var orderSpark1 = new ApexCharts(document.querySelector("#orderSpark1"), orderSpark1);
	orderSpark1.render();
	var orderSpark2 = new ApexCharts(document.querySelector("#orderSpark2"), orderSpark2);
	orderSpark2.render();
	var orderSpark3 = new ApexCharts(document.querySelector("#orderSpark3"), orderSpark3);
	orderSpark3.render();


	/* Revenue Spark Chart 1 */
	var revenueSpark1 = {
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
	/* Revenue Spark Chart 2 */
	var revenueSpark2 = {
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
	/* Revenue Spark Chart 3 */
	var revenueSpark3 = {
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

	var revenueSpark1 = new ApexCharts(document.querySelector("#revenueSpark1"), revenueSpark1);
	revenueSpark1.render();
	var revenueSpark2 = new ApexCharts(document.querySelector("#revenueSpark2"), revenueSpark2);
	revenueSpark2.render();
	var revenueSpark3 = new ApexCharts(document.querySelector("#revenueSpark3"), revenueSpark3);
	revenueSpark3.render();


	// Sales / Revenue Bar Chart
	var options = {
		chart: {
			height: 345,
			type: 'bar',
			fontFamily: 'IBM Plex Sans, sans-serif',
			foreColor: '#6780B1',
		},
		plotOptions: {
			bar: {
				horizontal: false,
				columnWidth: '55%',
				endingShape: 'rounded'
			},
		},
		dataLabels: {
			enabled: false
		},
		stroke: {
			show: true,
			width: 2,
			colors: ['transparent']
		},
		series: [{
			name: 'Net Profit',
			data: [44, 55, 57, 56, 61, 58, 63, 60, 66]
		}, {
			name: 'Revenue',
			data: [76, 85, 101, 98, 87, 105, 91, 114, 94]
		}, {
			name: 'Free Cash Flow',
			data: [35, 41, 36, 26, 45, 48, 52, 53, 41]
		}],
		colors: ['#5D78FF', '#C9D5FA', '#63CF72'],
		xaxis: {
			categories: ['Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct'],
		},
		yaxis: {
			title: {
				text: '$ (thousands)'
			}
		},
		fill: {
			opacity: 1

		},
		tooltip: {
			y: {
				formatter: function (val) {
					return "$ " + val + " thousands"
				}
			}
		}
	}

	var chart = new ApexCharts(
		document.querySelector("#salesRevenueBarChart"),
		options
	);

	chart.render();


});