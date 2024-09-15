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

	// Count Bar Chart
	window.Apex = {
		stroke: {
			width: 1
		},
		plotOptions: {
			bar: {
				columnWidth: '25%',
				endingShape: 'rounded'
			}
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
	var sparklineData = [47, 25, 54, 30, 56, 24, 50, 20, 47, 25, 54, 30, 56, 24, 50, 20, 47, 25, 54, 30, 56, 24, 50, 20, 47, 25, 54, 30, 56, 24, 50, 20];

	/* Count Spark 1 */
	var countSpark1 = {
		chart: {
			type: 'bar',
			height: 100,
			fontFamily: 'IBM Plex Sans, sans-serif',
			foreColor: '#6780B1',
			sparkline: {
				enabled: true
			},

		},

		markers: {
			size: 5,
			colors: undefined,
			strokeColors: '#E2ECFE',
			strokeWidth: 2,
			strokeOpacity: 0.9,
			fillOpacity: 1,
			discrete: [],
			shape: "circle",
			radius: 2,
			offsetX: 0,
			offsetY: 0,
			onClick: undefined,
			onDblClick: undefined,
			hover: {
				size: undefined,
				sizeOffset: 3
			}
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

	// data for the sparklines that appear below header area

	/* Count Spark 2 */
	var countSpark2 = {
		chart: {
			type: 'bar',
			height: 100,
			fontFamily: 'IBM Plex Sans, sans-serif',
			foreColor: '#6780B1',
			sparkline: {
				enabled: true
			},

		},

		markers: {
			size: 5,
			colors: undefined,
			strokeColors: '#6780B1',
			strokeWidth: 2,
			strokeOpacity: 0.9,
			fillOpacity: 1,
			discrete: [],
			shape: "circle",
			radius: 2,
			offsetX: 0,
			offsetY: 0,
			onClick: undefined,
			onDblClick: undefined,
			hover: {
				size: undefined,
				sizeOffset: 3
			}
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

		colors: ['#23BF08'],

	}

	/* Count Spark 3 */
	var countSpark3 = {
		chart: {
			type: 'bar',
			height: 100,
			fontFamily: 'IBM Plex Sans, sans-serif',
			foreColor: '#6780B1',
			sparkline: {
				enabled: true
			},

		},

		markers: {
			size: 5,
			colors: undefined,
			strokeColors: '#fff',
			strokeWidth: 1,
			strokeOpacity: 0.9,
			fillOpacity: 1,
			discrete: [],
			shape: "circle",
			radius: 2,
			offsetX: 0,
			offsetY: 0,
			onClick: undefined,
			onDblClick: undefined,
			hover: {
				size: undefined,
				sizeOffset: 3
			}
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

		colors: ['#F7AF17'],

	}

	var countSpark1 = new ApexCharts(document.querySelector("#countSpark1"), countSpark1);
	countSpark1.render();
	var countSpark2 = new ApexCharts(document.querySelector("#countSpark2"), countSpark2);
	countSpark2.render();
	var countSpark3 = new ApexCharts(document.querySelector("#countSpark3"), countSpark3);
	countSpark3.render();


	// Stastic Brush Chart
	$(function () {

		var data = generateDayWiseTimeSeries(new Date("15 Jul 2017").getTime(), 100, {
			min: 10,
			max: 20
		});
		var options1 = {
			chart: {
				id: "chart2",
				type: "area",
				height: 250,
				fontFamily: 'IBM Plex Sans, sans-serif',
				foreColor: '#6780B1',
				toolbar: {
					autoSelected: "pan",
					show: false
				}
			},
			colors: ["#4285F4"],
			stroke: {
				width: 1
			},
			grid: {
				borderColor: "#C8DCFC",
				clipMarkers: false,
				yaxis: {
					lines: {
						show: false
					}
				}
			},
			dataLabels: {
				enabled: false
			},
			fill: {
				gradient: {
					enabled: true,
					opacityFrom: 0.5,
					opacityTo: 0
				}
			},
			markers: {
				size: 5,
				colors: ["#4285F4"],
				strokeColor: "#fff",
				strokeWidth: 1
			},
			series: [{
				data: data
			}],
			tooltip: {
				theme: "dark"
			},
			xaxis: {
				type: "datetime"
			},
			yaxis: {
				min: 0,
				tickAmount: 5
			}
		};

		var chart1 = new ApexCharts(document.querySelector("#chart-area"), options1);

		chart1.render();


		var options2 = {
			chart: {
				id: "chart1",
				height: 150,
				type: "bar",
				fontFamily: 'IBM Plex Sans, sans-serif',
				foreColor: '#6780B1',
				brush: {
					target: "chart2",
					enabled: true
				},
				selection: {
					enabled: true,
					fill: {
						color: "#84BCFF",
						opacity: 0.5
					},
					xaxis: {
						min: new Date("27 Jul 2017 10:00:00").getTime(),
						max: new Date("14 Aug 2017 10:00:00").getTime()
					}
				}
			},
			plotOptions: {
				bar: {
					columnWidth: '25%',
					endingShape: 'rounded'
				}
			},

			dataLabels: {
				enabled: false
			},
			colors: ["#4285F4"],
			series: [{
				data: data
			}],
			stroke: {
				width: 1
			},
			grid: {
				borderColor: "#C8DCFC"
			},
			markers: {
				size: 0
			},
			xaxis: {
				type: "datetime",
				tooltip: {
					enabled: false
				}
			},
			yaxis: {
				tickAmount: 5
			}
		};

		var chart2 = new ApexCharts(document.querySelector("#chart-bar"), options2);

		chart2.render();

		function generateDayWiseTimeSeries(baseval, count, yrange) {
			var i = 0;
			var series = [];
			while (i < count) {
				var x = baseval;
				var y =
					Math.floor(Math.random() * (yrange.max - yrange.min + 1)) + yrange.min;

				series.push([x, y]);
				baseval += 86400000;
				i++;
			}
			return series;
		}


	});


	// Realtime Circle Chart
	window.Apex = {
		chart: {
			fontFamily: 'IBM Plex Sans, sans-serif',
			foreColor: '#6780B1',
			toolbar: {
				show: false
			},
		},
		colors: ['#FCCF31', '#17ead9', '#f02fc2'],
		stroke: {
			width: 1
		},
		dataLabels: {
			enabled: true
		},
		grid: {
			borderColor: "#40475D",
		},
		xaxis: {
			axisTicks: {
				color: '#333'
			},
			axisBorder: {
				color: "#333"
			}
		},
		fill: {
			type: 'gradient',
			gradient: {
				gradientToColors: ['#F55555', '#6078ea', '#6094ea']
			},
		},
		tooltip: {
			theme: 'dark',
			x: {
				formatter: function (val) {
					return moment(new Date(val)).format("HH:mm:ss")
				}
			}
		},
		yaxis: {
			decimalsInFloat: 2,
			opposite: true,
			labels: {
				offsetX: -10
			}
		}
	};

	var trigoStrength = 3
	var iteration = 11

	function getRandom() {
		var i = iteration;
		return (Math.sin(i / trigoStrength) * (i / trigoStrength) + i / trigoStrength + 1) * (trigoStrength * 2)
	}

	function getRangeRandom(yrange) {
		return Math.floor(Math.random() * (yrange.max - yrange.min + 1)) + yrange.min
	}

	function generateMinuteWiseTimeSeries(baseval, count, yrange) {
		var i = 0;
		var series = [];
		while (i < count) {
			var x = baseval;
			var y = ((Math.sin(i / trigoStrength) * (i / trigoStrength) + i / trigoStrength + 1) * (trigoStrength * 2))

			series.push([x, y]);
			baseval += 300000;
			i++;
		}
		return series;
	}


	function getNewData(baseval, yrange) {
		var newTime = baseval + 300000;
		return {
			x: newTime,
			y: Math.floor(Math.random() * (yrange.max - yrange.min + 1)) + yrange.min
		}
	}


	var optionsCircle = {
		chart: {
			type: 'radialBar',
			height: 250,
			offsetY: 0,
			offsetX: 0
		},
		plotOptions: {
			radialBar: {
				size: undefined,
				inverseOrder: false,
				hollow: {
					margin: 1,
					size: '48%',
					background: 'transparent',
				},
				track: {
					show: true,
					background: '#C8DCFC',
					strokeWidth: '10%',
					opacity: 1,
					margin: 3, // margin is in pixels
				},


			},
		},
		series: [55, 60],
		labels: ['CPU', 'RAM'],
		legend: {
			show: true,
			position: 'left',
			offsetX: 0,
			offsetY: 0,
			formatter: function (val, opts) {
				return val + " - " + opts.w.globals.series[opts.seriesIndex] + '%'
			}
		},
		fill: {
			type: 'gradient',
			gradient: {
				shade: 'dark',
				type: 'horizontal',
				shadeIntensity: 0.1,
				inverseColors: true,
				opacityFrom: 1,
				opacityTo: 1,
				stops: [0, 100]
			}
		},
		stroke: {
			lineCap: 'round'
		}
	}

	var chartCircle = new ApexCharts(document.querySelector('#circlechart'), optionsCircle);
	chartCircle.render();


	window.setInterval(function () {

		iteration++;


		chartCircle.updateSeries([getRangeRandom({
			min: 10,
			max: 100
		}), getRangeRandom({
			min: 10,
			max: 100
		})])


	}, 3000)


	// Region GeoCharts
	google.charts.load('current', {
		'packages': ['geochart'],
		// Note: you will need to get a mapsApiKey for your project.
		// See: https://developers.google.com/chart/interactive/docs/basic_load_libs#load-settings
		'mapsApiKey': 'AIzaSyD-9tSrke72PouQMnMX-a7eZSW0jkFMBWY'
	});
	google.charts.setOnLoadCallback(drawRegionsMap);

	function drawRegionsMap() {
		var data = google.visualization.arrayToDataTable([
			['Country', 'Popularity'],
			['Germany', 200],
			['United States', 300],
			['Brazil', 400],
			['Canada', 500],
			['France', 600],
			['RU', 700]
		]);

		var options = {
			colors: ['#5D78FF', '#C9D5FA'],
		};

		var chart = new google.visualization.GeoChart(document.getElementById('regionGeoCharts'));

		chart.draw(data, options);
	}


	// Area Spark Chart
	window.Apex = {
		stroke: {
			width: 1
		},
		plotOptions: {
			bar: {
				columnWidth: '25%',
				endingShape: 'rounded'
			}
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
	var sparklineData = [147, 125, 154, 130, 156, 124, 150, 120, 147, 125, 154, 154, 130, 156, 124, 150, 120];

	// Area Spark 1 
	var areaSpark1 = {
		chart: {
			type: 'area',
			height: 150,
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


	// Area Spark 2 
	var areaSpark2 = {
		chart: {
			type: 'area',
			height: 150,
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

		colors: ['#23BF08'],

	}


	var areaSpark1 = new ApexCharts(document.querySelector("#areaSpark1"), areaSpark1);
	areaSpark1.render();
	var areaSpark2 = new ApexCharts(document.querySelector("#areaSpark2"), areaSpark2);
	areaSpark2.render();


});