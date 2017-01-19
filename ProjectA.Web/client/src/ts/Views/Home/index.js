/// <reference path="../../../../_references.ts" />
var Views;
(function (Views) {
    var Index = (function () {
        function Index() {
            this.meters = [];
            var meter1 = $("#PM25");
            var meter2 = $("#PM10");
            this.meters.push(new Componenets.WheelMeter(meter1));
            this.meters.push(new Componenets.WheelMeter(meter2));
        }
        return Index;
    }());
    Views.Index = Index;
})(Views || (Views = {}));
//# sourceMappingURL=index.js.map