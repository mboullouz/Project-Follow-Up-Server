module Entity {
    export class Task {
        id: number;
        idProject: number;
        title: string;
        description: string;
        priority: number;
        ditionNumber: number;
        done: boolean;
        createAt: Date;
        editAt: Date;
        finishAt: Date;
        constructor() {
            console.log("Task 'Entity' loaded! ");
        }

    }
    export class TaskBasic {
        private Id: number;
        private Done: boolean;
        constructor(id?: number, done?: boolean) {
            this.Done = done;
            this.Id = id;
        }
        
        public getId():number{
            return this.Id;
        }
        public setId(value: number) {
            this.Id= value;
        }
        public getDone(): boolean {
            return this.Done;
        }
        public setDone(value: boolean) {
            this.Done = value;
        }

    }

    export class NotificationBasic {
        private Id: number;
        private Seen: boolean;
        constructor(id?: number, seen?: boolean) {
            this.Seen = seen;
            this.Id = id;
        }

        public getId(): number {
            return this.Id;
        }
        public setId(value: number) {
            this.Id = value;
        }
        public getDone(): boolean {
            return this.Seen;
        }
        public setDone(value: boolean) {
            this.Seen = value;
        }
    }

}