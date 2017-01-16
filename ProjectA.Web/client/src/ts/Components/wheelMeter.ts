/// <reference path="../../../_references.ts" />

module Componenets {
    export class WheelMeter {
        private $root: JQuery;
        private $title: JQuery;
        private $circle: JQuery;

        constructor($root: JQuery) {
            this.$root = $root;
            this.queryElements();
            this.initClickHandlers();
        }

        private queryElements() {
            this.$title = this.$root.find(".title");
            this.$circle = this.$root.find(".circle");
        }

        private initClickHandlers() {
            let self = this;
            this.$circle.on("click", function () {
                self.$title.text(self.$title.text() + "1");
            });
        }
    }
}