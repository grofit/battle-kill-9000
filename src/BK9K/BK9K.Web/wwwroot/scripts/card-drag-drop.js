const ViewTypes = {
    Unknown: 0,
    Unit: 1,
    Tile: 2,
    WeaponSlot: 3,
    ArmourSlot: 4,
    AbilitySlot: 5
}

const CardTypes = {
    Unknown: 0,
    RaceCard: 1,
    ClassCard: 2,
    ItemCard: 3,
    EquipmentCard: 4,
    EffectCard: 5,
    AbilityCard: 6,
    SpellCard: 7
}

const defaultLineColor = "#999";
const draw = SVG().addTo('body').size(1920, 1080);
const line = draw.line(0, 0, 0, 0).stroke({ width: 10, color: defaultLineColor, opacity: 0.6 });
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
    switch (viewType) {
        case ViewTypes.Unit: return canCardBeUsedOnUnit(cardType);
        case ViewTypes.Tile: return canCardBeUsedOnTile(cardType);
        default: false;
    }
}

function canCardBeUsedOnUnit(cardType) {
    switch (cardType) {
    case CardTypes.SpellCard:
        return true;
    default:
        return false;
    }
}

function canCardBeUsedOnTile(cardType) {
    switch (cardType) {
    case CardTypes.Unknown:
        return false;
    default:
        return true;
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

    const viewType = evt.toElement.getAttribute("view-type");
    const viewId = evt.toElement.getAttribute("view-id");
    const cardType = currentDragState.cardType;

    const canBeUsed = canCardBeUsedOnView(cardType, viewType);
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
    if (currentDragState.canBeUsed == true) {

        runInterop((dotNet) => {
            dotNet.invokeMethod("ApplyCard",
                currentDragState.cardViewId,
                currentDragState.targetViewType,
                currentDragState.targetViewId);
        });
    }
}

function OnDragOver(evt) {
    evt.preventDefault();
}

function OnCardDragLeave(evt) {
    childCounter--;
    if (childCounter === 0) {
        line.stroke(defaultLineColor);
        currentDragState.canBeUsed = false;
    }
}