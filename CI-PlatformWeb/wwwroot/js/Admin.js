var dashboadtime = document.getElementById("dashboadtime");
function currentTime() {
    var time = new Date();
    var dateString = time.toDateString();
    var timeString = time.toLocaleTimeString();
    dashboadtime.innerHTML = dateString + ' ' + timeString;
    let t = setTimeout(function () { currentTime() }, 1000);
}
currentTime();
$(document).ready(function () {
    $('#example').DataTable();
});
CKEDITOR.replace('editor1');

function ved1() {
	var a = document.getElementById("s1");
	var c = document.getElementById("s2");
	var b = a.options[a.selectedIndex];
	for (var i = 0; i < a.length; i++) {

		if (a.options[i].selected == true) {
			a.options[i].selected = false
			c.add(a.options[i])

			ved1()
		}

	}
}
function ved2() {
	var a = document.getElementById("s1");
	var c = document.getElementById("s2");
	var b = c.options[c.selectedIndex];
	for (var i = 0; i < c.length; i++) {
		if (c.options[i].selected == true) {
			c.options[i].selected = false
			a.add(c.options[i])
			ved2()
		}
	}
}
function ved3() {
	var a = document.getElementById("s1");
	var c = document.getElementById("s2");
	for (var i = 0; i < a.length;) {
		c.add(a.options[c, i])
	}
}
function ved4() {
	var a = document.getElementById("s1");
	var c = document.getElementById("s2");
	for (var i = 0; i < c.length;) {
		a.add(c.options[a, i])
	}
}
