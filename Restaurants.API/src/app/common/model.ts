export abstract class BaseModel {
    id: number;
    createdByUserId: number;
    createdBy: People = {} as People;
    createdDateTime: string;
    modifiedByUserId: number;
    modifiedBy: People = {} as People;
    modifiedDateTime: string;
}

export class Restaurant extends BaseModel {
    name: string;
    description: string;
}

export class EmployersRestaurants extends BaseModel {
    restaurantId: number;
    theEmployer: Employers;
    employerId: number;
    theRestaurant: Restaurant;
}

export class Employers extends BaseModel {
    personId: number;
    theEmployerDetails: People;
}

export class People extends BaseModel {
    personFirstName: string;
    personMiddleName: string;
    personLastName: string;
}

export class Tupple<IFirst, ISecond> {
    item1: IFirst;
    item2: ISecond;
}

export class Phone extends BaseModel {
    restaurantId: number;
    phoneNumber: string;
    phoneDescription: string;
}