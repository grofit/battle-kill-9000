var draw = SVG().addTo('body').size(1920, 1080);
var line = draw.line(0,0,0,0).stroke({ width: 10, color: "#999", opacity: 0.6 });
line.attr("stroke-dasharray", 4);
line.attr("stroke-linecap", "round");

var dummyImg = document.createElement("img");
var childCounter = 0;

var currentDragState = {};

function getElementPosition(element) {
    var bodyRect = document.body.getBoundingClientRect(),
        elemRect = element.getBoundingClientRect();

    var offsetRect = {
        top: elemRect.top - bodyRect.top,
        bottom: elemRect.bottom - bodyRect.bottom,
        left: elemRect.left - bodyRect.left,
        right: elemRect.right - bodyRect.right,
        width: elemRect.width,
        height: elemRect.height
    };
    return offsetRect;
}

function canCardBeUsedOnView(cardType, viewType) {
    if (viewType == 1) {
        switch (cardType) {
        case 0:
        case 1:
        case 2:
        case 7:
            return false;
        default:
            return true;
        }
    }
}

function OnStartDragCard(evt) {
    currentDragState = { cardType: evt.target.getAttribute("card-type"), cardViewId: evt.target.getAttribute("view-id") };
    evt.dataTransfer.setDragImage(dummyImg, 0, 0);
    var rect = getElementPosition(evt.target);
    line.attr("x1", rect.left + (rect.width / 2));
    line.attr("y1", rect.top);
}

function OnEndDragCard(evt) {
    line.attr("x1", 0);
    line.attr("y1", 0);
    line.attr("x2", 0);
    line.attr("y2", 0);
}

function OnDragging(evt) {
    line.attr("x2", evt.pageX);
    line.attr("y2", evt.pageY);
}

function OnCardDragEnter(evt) {
    childCounter++;
    evt.preventDefault();
    if (!evt.toElement.hasAttribute("view-type")) {
        return;
    }

    var viewType = evt.toElement.getAttribute("view-type");
    var viewId = evt.toElement.getAttribute("view-id");
    var cardType = currentDragState.cardType;

    var canBeUsed = canCardBeUsedOnView(cardType, viewType);
    if (canBeUsed) {
        line.stroke("#0F0");
        currentDragState.targetViewType = viewType;
        currentDragState.targetViewId = viewId;
        currentDragState.canBeUsed = true;
    } else {
        line.stroke("#F00");
        currentDragState.canBeUsed = false;
    }
}

function OnCardDrop(evt) {
    console.log("DROPPING", currentDragState);
    if (currentDragState.canBeUsed == true) {

        runInterop((dotNet) => {
            dotNet.invokeMethod("ApplyCard",
                currentDragState.cardViewId,
                currentDragState.targetViewType,
                currentDragState.targetViewId);
            console.log("CALLED DOTNET");
        });
    }
}

function OnDragOver(evt) {
    evt.preventDefault();
}

function OnCardDragLeave(evt) {
    childCounter--;
    if (childCounter === 0) {
        line.stroke("#999");
        currentDragState.canBeUsed = false;
        console.log("LEAVING");
    }
}