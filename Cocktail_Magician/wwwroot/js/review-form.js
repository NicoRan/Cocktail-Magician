//let buttonId = "#show-hide-review";
$(document).ready(() => {
    console.log('vatre');
    $('#show-hide-review').on('click', function (event) {
        console.log(event);
        let divHolder = document.getElementById('review-show');
        console.log(divHolder);
        if (divHolder.style.display === 'none') {
            divHolder.style.display = "initial";
        }
        else {
            divHolder.style.display = "none";
        }
    });
});
