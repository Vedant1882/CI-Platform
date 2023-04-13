let slideIndex = 1;
showSlides(slideIndex);

// Next/previous controls
function plusSlides(n) {
    showSlides(slideIndex += n);
}

// Thumbnail image controls
function currentSlide(n) {
    showSlides(slideIndex = n);
}

function showSlides(n) {
    let i;
    let slides = document.getElementsByClassName("mySlides");
    let dots = document.getElementsByClassName("demo");
/*    let captionText = document.getElementById("caption");*/
    if (n > slides.length) { slideIndex = 1 }
    if (n < 1) { slideIndex = slides.length }
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace(" active", "");
    }
    slides[slideIndex - 1].style.display = "block";
    dots[slideIndex - 1].className += " active";
/*    captionText.innerHTML = dots[slideIndex - 1].alt;*/
}

function sendmail(id) {
    const mail = Array.from(document.querySelectorAll('input[name="mail"]:checked')).map(el => el.id);
    var send = document.getElementById("sendmail");
    Swal.fire({
        icon: 'info',
        title: 'Sending',
        text: 'Sending Mail....',
        showConfirmButton: false,
        
    })
    $.ajax({
        url: '/Employee/Home/Sendmail',
        type: 'POST',
        data: { userid: mail ,id:id},
        success: function (result) {
            
            const checkboxes = document.querySelectorAll('input[name="mail"]:checked');
            checkboxes.forEach((checkbox) => {
                checkbox.checked = false;
            });
            Swal.fire({
                icon: 'success',
                title: 'Sent',
                text: 'Recommandation sent succesfully',
                showConfirmButton: false,
                timer: 2000
            })
        },
        error: function () {
            // Handle error response from the server, e.g. show an error message to the user
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Something went wrong!',
                
            })
        }
    });

}