$(document).on("click", ".add-to-basket", function (e) {
    e.preventDefault();

    let link = $(this).attr("href");

    fetch(link)
        .then(response => {
            if (!response.ok) {
                alert("something went Wrong");
                throw new Error("not ok response")
                return;
            }
            return response.text();
        }).then(html => {
            console.log(html)
            $("#BasketHolder").html(html);
        })

})