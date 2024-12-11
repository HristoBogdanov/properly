import { Dispatch, SetStateAction } from "react";

type DialogProps = {
  text: string;
  isDailogOpen: boolean;
  setIsDailogOpen: Dispatch<SetStateAction<boolean>>;
  onConfirmAction: () => void;
};

export default function Dialog({
  text,
  isDailogOpen,
  setIsDailogOpen,
  onConfirmAction,
}: DialogProps) {
  return (
    <>
      {isDailogOpen && (
        <div className="fixed top-0 left-0 w-full h-full flex justify-center items-center bg-black bg-opacity-50 z-50">
          <div className="bg-white p-6 rounded-lg shadow-lg max-w-sm w-full">
            <h3 className="text-2xl font-bold mb-4">{text}</h3>
            <div className="flex justify-end gap-4">
              <button
                className="px-4 py-2 bg-green-600 text-white rounded hover:bg-green-700"
                onClick={() => {
                  onConfirmAction();
                  setIsDailogOpen(false);
                }}
              >
                Confirm
              </button>
              <button
                className="px-4 py-2 bg-red-600 text-white rounded hover:bg-red-700"
                onClick={() => setIsDailogOpen(false)}
              >
                Cancel
              </button>
            </div>
          </div>
        </div>
      )}
    </>
  );
}
