
function deletetimesheet(timesheetid) {

    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: true
    })

    swalWithBootstrapButtons.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this timesheet!",
        icon: 'warning',
        width: '300',
        height: '100',
        showCancelButton: true,
        confirmButtonText: 'Yes',
        cancelButtonText: 'No',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/Employee/Home/deletetimesheet',
                type: 'POST',
                data: { timesheetid: timesheetid },

                success: function (response) {

                    swalWithBootstrapButtons.fire(
                        'Deleted!',
                        'Your timesheet has been deleted.',
                        'success'
                    )
                    $('.timesheetdiv').html($(response).find('.timesheetdiv').html());


                },
                error: function () {
                    alert("could not comment");
                }
            });

        } else if (
            /* Read more about handling dismissals below */
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire(
                'Cancelled',

            )
        }
    })


    

}
function deletegoalsheet(timesheetid) {


    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: true
    })

    swalWithBootstrapButtons.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this goalsheet!",
        icon: 'warning',
        width: '300',
        height: '100',
        showCancelButton: true,
        confirmButtonText: 'Yes',
        cancelButtonText: 'No',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/Home/deletetimesheet',
                type: 'POST',
                data: { timesheetid: timesheetid },

                success: function (response) {

                    swalWithBootstrapButtons.fire(
                        'Deleted!',
                        'Your goalsheet has been deleted.',
                        'success'
                    )
                    $('.goalsheetdiv').html($(response).find('.goalsheetdiv').html());


                },
                error: function () {
                    alert("could not comment");
                }
            });

        } else if (
            /* Read more about handling dismissals below */
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire(
                'Cancelled',

            )
        }
    })





}
function editgoalsheet(timesheetid) {

    $.ajax({
        url: '/Home/editsheet',
        type: 'POST',
        data: { timesheetid: timesheetid },

        success: function (response) {
            console.log(response.timesheet.action);
            var ele = document.getElementById('action1');
            ele.value = response.timesheet.action;
            var mission = document.getElementById('mission1');
            mission.value = response.timesheet.missionId;
            var date = document.getElementById('date2');
            date.value = response.timesheet.dateVolunteered;
            var notes = document.getElementById('notes1');
            notes.value = response.timesheet.notes;
            var timesheetid1 = document.getElementById('timesheetid2');
            timesheetid1.value = response.timesheet.timesheetId;
            /*ele.innerText = response.timesheet.action;*/




        },
        error: function () {
            alert("could not comment");
        }
    });

}
function edittimesheet(timesheetid) {

    $.ajax({
        url: '/Home/editsheet',
        type: 'POST',
        data: { timesheetid: timesheetid },

        success: function (response) {
            console.log(response.timesheet.action);
            var hour = document.getElementById('hour1');
            hour.value = String(response.timesheet.timesheetTime).split(":")[0];
            var minute = document.getElementById('minute1');
            minute.value = String(response.timesheet.timesheetTime).split(":")[1];
            var mission = document.getElementById('mission');
            mission.value = response.timesheet.missionId;
            var date = document.getElementById('date1');
            date.value = response.timesheet.dateVolunteered;
            var notes = document.getElementById('notes');
            notes.value = response.timesheet.notes;
            var timesheetid1 = document.getElementById('timesheetid1');
            timesheetid1.value = response.timesheet.timesheetId;
            /*ele.innerText = response.timesheet.action;*/



        },
        error: function () {
            alert("could not comment");
        }
    });

}
