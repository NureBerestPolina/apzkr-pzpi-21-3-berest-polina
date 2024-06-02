export interface RegisterRequest {
    email: string;
    password: string;
    name: string;
    organizationName: string;
    description: string | null;
    role: string;
}