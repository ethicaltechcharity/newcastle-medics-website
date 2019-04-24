$(".navbar").hide();
$(".footer-div").hide();
$("body").css('padding', '0');
$(".top-num").on("click", function (e) {
    e.preventDefault();
    this.classList.add("active");
});
