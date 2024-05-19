import { Cell } from "./cell.model";

export interface TechInspectionRequest {
    id: string;
    requestedTime: Date;
    fulfilledTime: Date | null;
    status: string;
    cell: Cell;
    temperature: number;
    opensCount: number;
}