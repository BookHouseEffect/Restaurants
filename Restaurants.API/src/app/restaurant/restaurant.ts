export class Restaurant {
    id: number;
    name: string;
    description: string;
    createdByUserId: number;
    createdDateTime: string;
    modifiedByUserId: number;
    modifiedDateTime: string;
}

export class EmployersRestaurants {
    id: number;
    restaurantId: number;
    employerId: number;
}

export class Tupple<IFirst, ISecond> {
    item1: IFirst;
    item2: ISecond;
}