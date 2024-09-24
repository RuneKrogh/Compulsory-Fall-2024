// src/atoms.ts
import { atom } from 'jotai';
import { Order } from '../Api.ts'; // Adjust the import based on your structure

export const ordersAtom = atom<Order[]>([]);
