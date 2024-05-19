import { Order } from "./order.model";
import { PickupCounter } from "./pickup-counter.model";

export interface Cell {
    id: string;
    isFree: boolean;
    hasTemperatureControl: boolean;
    counterId: string;
    counter: PickupCounter;
    cellOpenKey: string;
    order: Order;   
}