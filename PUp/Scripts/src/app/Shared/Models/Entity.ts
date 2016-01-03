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

}