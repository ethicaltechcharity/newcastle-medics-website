var canvas;
canvas = document.getElementsByClassName("formation-canvas")[0];
var ctx = canvas.getContext("2d");
var lineWidth = 4;
var canvasWidth = canvas.width;
canvas.height = (5 / 3) * canvasWidth;
var canvasHeight = canvas.height;
var sidePitchBorder = canvasWidth / 20;
var verticalPitchBorder = (5 / 3) * (canvasWidth / 20);
var pitchWidth = canvasWidth - 2 * sidePitchBorder;
var pitchHeight = canvasHeight - 2 * verticalPitchBorder;
var pixelsToMetre = pitchWidth / 55;
ctx.fillStyle = "#a11caf";
ctx.fillRect(0, 0, canvasWidth, canvasHeight);
ctx.fillStyle = "#369b3e";
ctx.fillRect(sidePitchBorder, verticalPitchBorder, pitchWidth, pitchHeight);
ctx.fillStyle = "#FFF";
ctx.fillRect(sidePitchBorder, verticalPitchBorder, pitchWidth, lineWidth);
ctx.fillRect(sidePitchBorder, verticalPitchBorder, lineWidth, pitchHeight);
ctx.fillRect(sidePitchBorder + pitchWidth - lineWidth, verticalPitchBorder, lineWidth, pitchHeight);
ctx.fillRect(sidePitchBorder, verticalPitchBorder + pitchHeight - lineWidth, pitchWidth, lineWidth);
ctx.fillRect(sidePitchBorder, verticalPitchBorder + 22.9 * pixelsToMetre - lineWidth / 2, pitchWidth, lineWidth);
ctx.fillRect(sidePitchBorder, verticalPitchBorder + (22.9 + 22.8) * pixelsToMetre - lineWidth / 2, pitchWidth, lineWidth);
ctx.fillRect(sidePitchBorder, verticalPitchBorder + (22.9 + 22.8 + 22.8) * pixelsToMetre - lineWidth / 2, pitchWidth, lineWidth);
function drawPitch(canvasWidth) {
}
//# sourceMappingURL=formation.js.map