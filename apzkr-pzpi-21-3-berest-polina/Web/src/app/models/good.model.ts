import { Category } from "./category.model";
import { Organization } from "./organization.model";
import { Producer } from "./producer.model";

export interface Good {
    id: string;
    name: string;
    description: string;
    measurementUnit: string;
    needsCooling: boolean;
    price: number;
    amountInStock: number;
    producer: Producer;
    category: Category;
    organization: Organization;
}