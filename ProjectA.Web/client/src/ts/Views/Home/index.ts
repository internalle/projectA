/// <reference path="../../../../_references.ts" />

module Views {
    export class Index {
        private meters: Componenets.WheelMeter[];

        constructor() {
            this.meters = [];

            let meter1 = $("#PM25");
            let meter2 = $("#PM10");

            this.meters.push(new Componenets.WheelMeter(meter1));
            this.meters.push(new Componenets.WheelMeter(meter2));
        }
    }
}