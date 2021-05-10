var runInterop;

var interopProxy = interopProxy || {};
interopProxy.exposeInterop = function (dotNetObjRef) {

    console.log("got reference", dotNetObjRef);
    function underlyingInteropHandler(method) {
        method(dotNetObjRef);
    }

    runInterop = underlyingInteropHandler;
}