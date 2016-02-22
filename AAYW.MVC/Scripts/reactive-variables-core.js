function ReactiveVariable(data) {
    this._data = data;
    this._beforeSet = [];
    this._beforeGet = [];
    this._afterSet = [];
}

ReactiveVariable.BeforeGet = "bS";
ReactiveVariable.BeforeSet = "bG";
ReactiveVariable.AfterSet = "aS";

ReactiveVariable.Initialize = function () {
    var scopedDOM = document.querySelectorAll('*[data-reactive="true"]');

    var length = scopedDOM.length;
    for (var i = 0; i < length; i++) {
        var variableName = "__rv-"+scopedDOM[i].dataset["reactiveTarget"];

        if (!window[variableName])
            window[variableName] = new ReactiveVariable(null);
        else if (!(window[variableName] instanceof ReactiveVariable))
            throw new Error("[ReactiveVariable] Target must be ReactiveVariable");

        var method = scopedDOM[i].dataset["reactiveMethod"] || "change";

        scopedDOM[i]["on" + method] = (function (variableName) {
            return function () {
                window[variableName].Data = this.value;
            };
        })(variableName);
    }
};

ReactiveVariable.prototype = {
    Bind: function (type, handler) {
        if (!(handler instanceof Function)) {
            throw new Error("[ReactiveVariable] Handler is not a function");
        }

        var handlerWrapper = {
            handler: handler,
            id: Math.random(),
            call: function (self) {
                handler.call(self);
            }
        };
        switch (type) {
            case ReactiveVariable.BeforeGet:
                this._beforeGet.push(handlerWrapper);
                break;
            case ReactiveVariable.BeforeSet:
                this._beforeSet.push(handlerWrapper);
                break;
            case ReactiveVariable.AfterSet:
                this._afterSet.push(handlerWrapper);
                break;
            default:
                throw new Error("[ReactiveVariable] Unknown event type");
        }
        return handlerWrapper.id;
    },
    Unbind: function (type, id) {
        var comp = function (element) {
            return element.id != id;
        };

        switch (type) {
            case ReactiveVariable.BeforeGet:
                this._beforeGet = this._beforeGet.filter(comp);
                break;
            case ReactiveVariable.BeforeSet:
                this._beforeSet = this._beforeSet.filter(comp);
                break;
            case ReactiveVariable.AfterSet:
                this._afterSet = this._afterSet.filter(comp);
                break;
            default:
                throw new Error("[ReactiveVariable] Unknown event type");
        }
    },
    get Data() {
        var _self = this;
        this._beforeGet.forEach(function (func) {
            func.call(_self._data);
        });
        return this._data;
    },
    set Data(value) {
        var _self = this;
        this._beforeSet.forEach(function (func) {
            func.call(_self._data);
        });

        this._data = value;

        this._afterSet.forEach(function (func) {
            func.call(_self._data);
        });

        return value;
    }
};
