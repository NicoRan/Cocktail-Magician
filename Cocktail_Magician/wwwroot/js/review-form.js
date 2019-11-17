//let buttonId = "#show-hide-review";
$(document).ready(() => {
    console.log('vatre');
    $('#show-hide-review').on('click', function (event) {
        console.log(event);
        let divHolder = document.getElementById('review-show');
        console.log(divHolder);
        if (divHolder.style.visibility === 'hidden') {
            divHolder.style.visibility = "visible";
        }
        else {
            divHolder.style.visibility = "hidden";
        }
    });
});
