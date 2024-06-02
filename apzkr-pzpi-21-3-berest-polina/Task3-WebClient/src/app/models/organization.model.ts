import { User } from "./auth/user.model";
import { PickupCounter } from "./pickup-counter.model";

export interface Organization {
    id: string;
    name: string;
    description: string | null;
    registerDate: Date;
    manager?: User;
    pickupCounters?: PickupCounter[];
    isBlocked?: boolean;
}
