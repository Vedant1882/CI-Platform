

function deletemission(missionId) {


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
                url: '/Admin/Admin/DeleteMission',
                type: 'POST',
                data: { missionId: missionId },

                success: function (response) {

                    swalWithBootstrapButtons.fire(
                        'Deleted!',
                        'Your goalsheet has been deleted.',
                        'success'
                    )
                    $('#example').html($(response).find('#example').html());
                    location.reload();


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


function SaveSkills() {
    var selectedSkills = [];
    var selected = "";
    const skillsSelected = $('#s2 option');

    for (var i = 0; i < skillsSelected.length; i++) {
        selectedSkills.push(skillsSelected[i].value);
        console.log(skillsSelected[i].value)
    }

    selected = selectedSkills.toString();
    console.log(selected)

    document.getElementById('selectedSkills').value = selected;

};

function reload() {
    location.reload();
}
function editmission(missionId) {

    $.ajax({
        url: '/Admin/Admin/GetMission',
        type: 'POST',
        data: { missionId: missionId },

        success: function (response) {
            console.log(response)
            //document.getElementById("themeName").value = response.title;
            $('#AdminMissionmodal').html($(response).find('#AdminMissionmodal').html());
           

        },
        error: function () {
            alert("could not comment");
        }
    });
}
function showSave() {
    document.getElementById("missionSave").style.display = "";
    document.getElementById("missionSave1").style.display = "none";
}
function goalchange() {
    if (document.getElementById("missionType").value == "goal") {
        document.getElementById("goal").style.display = "";
        document.getElementById("time").style.display = "none";
    } else {
        document.getElementById("goal").style.display = "none";
        document.getElementById("time").style.display = "block";

    }

}


$(document).ready(function () {
   

    myDropzone.on('sendingmultiple', function () {
        // Add any additional data you want to send with the request
        var data = $('#my-awesome-dropzone').serializeArray();
        $.each(data, function (key, el) {
            myDropzone.options.params[el.name] = el.value;
        });
    });

    myDropzone.on('successmultiple', function (files, response) {
        // Do something when the upload is successful
    });
});

function removedoc(docId){
    $.ajax({
        url: '/Admin/Admin/delDoc',
        type: 'POST',
        data: { docId: docId },

        success: function (response) {


            document.getElementById(docId).style.display = "none";

        },
        error: function () {
            alert("could not comment");
        }
    });
}

function removeimg(imgId) {
    $.ajax({
        url: '/Admin/Admin/delImg',
        type: 'POST',
        data: { imgId: imgId },

        success: function (response) {

            //document.getElementById("themeName").value = response.title;
            /*            $('#AdminMissionmodal').html($(response).find('#AdminMissionmodal').html());*/
            document.getElementById(imgId).style.display = "none";

        },
        error: function () {
            alert("could not comment");
        }
    });
}