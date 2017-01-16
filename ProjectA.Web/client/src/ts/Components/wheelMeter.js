/// <reference path="../../../_references.ts" />
var Componenets;
(function (Componenets) {
    var WheelMeter = (function () {
        function WheelMeter($root) {
            this.$root = $root;
            this.queryElements();
        }
        WheelMeter.prototype.queryElements = function () {
            this.$title = this.$root.find(".title");
            this.$circle = this.$root.find(".circle");
        };
        WheelMeter.prototype.initClickHandlers = function () {
            var self = this;
            this.$circle.on("click", function () {
                self.$title.text(self.$title.text() + "1");
            });
        };
        return WheelMeter;
    }());
    Componenets.WheelMeter = WheelMeter;
})(Componenets || (Componenets = {}));
//# sourceMappingURL=wheelMeter.js.map