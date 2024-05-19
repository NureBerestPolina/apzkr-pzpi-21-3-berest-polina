import { User } from "./auth/user.model";
import { Cell } from "./cell.model";

export interface Order {
    id: string;
    orderedDate: Date;
    finalizedDate: Date | null;
    status: string;
    assignedCellId: string;
    assignedCell: Cell;
    customerId: string;
    customer: User;
}