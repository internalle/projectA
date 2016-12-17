/// <reference path="../../../_references.ts" />
var Views;
(function (Views) {
    var Shared = (function () {
        function Shared() {
            var $ = jQuery;
            $(".ui.dropdown").dropdown();
        }
        return Shared;
    }());
    Views.Shared = Shared;
})(Views || (Views = {}));
//# sourceMappingURL=shared.js.map