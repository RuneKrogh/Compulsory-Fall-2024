import {atom} from 'jotai';
import {Paper} from "../components/main/Api.ts";

// Define the cart items atom with a proper type
export const shoppingCartAtom = atom<Paper[]>([]); // An array of Paper items
