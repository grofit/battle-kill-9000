var runInterop;

var interopProxy = interopProxy || {};
interopProxy.exposeInterop = function (dotNetObjRef) {
    
    function underlyingInteropHandler(method) {
        method(dotNetObjRef);
    }

    runInterop = underlyingInteropHandler;
}