/// <reference path="../../../_references.ts" />

module Views {
    export class Shared {
        constructor() {
            let $: any = jQuery;
            $(".ui.dropdown").dropdown();
        }
    }
}